using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;
using Windows.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using ComicsAPI.Data;
using Newtonsoft.Json;

namespace MarvelAPI
{
    public abstract class ClientBase
    {
        protected const string CharactersResource = "characters";
        protected const string ComicsResource = "comics";
        protected const string CreatorsResource = "creators";
        protected const string EventsResource = "events";
        protected const string SeriesResource = "series";
        protected const string StoriesResource = "stories";

        internal readonly Uri BaseUrl = new Uri("http://gateway.marvel.com/v1/public/");
        private readonly string _publicKey;
        private readonly string _privateKey;

        internal ClientBase(string publicKey, string privateKey)
        {
            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public async Task<Rootobject> Find(int id)
        {
            var task = await QueryIdSubPath(id);
            return await DeserializeToRootobject(task);
        }

        public async Task<Rootobject> FindAll(int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            var options = queryParameters ?? new Dictionary<string,string>();
            options.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
            options.Add("offset", offset.ToString(CultureInfo.InvariantCulture));
            var task = await Query(Resource, options);
            return await DeserializeToRootobject(task);
        }

        public static Task<Rootobject> DeserializeToRootobject(string json)
        {
            json = json.Replace("-0001-11-30T00:00:00-0500", DateTime.MinValue.ToString());
            Task<Rootobject> task = Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Rootobject>(json));
            return task;
        }

        protected Task<string> QueryIdSubPath(int id, string path = null, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            var options = queryParameters ?? new Dictionary<string,string>();
            options.Add("limit", limit.ToString(CultureInfo.InvariantCulture));
            options.Add("offset", offset.ToString(CultureInfo.InvariantCulture));

            var resourceUri = Resource + "/" + id;
            
            if(!string.IsNullOrEmpty(path))
            {
                resourceUri += "/" + path;
            }
            return Query(resourceUri, options);
        }

        private async Task<string> Query(string resourcePath, Dictionary<string,string> options, Dictionary<string,string> urlSegments = null)
        {
            HttpWebRequest request = PrepareRequest(resourcePath, options, urlSegments);
            Task<WebResponse> task = Task.Factory.FromAsync(
                request.BeginGetResponse,
                asyncResult => request.EndGetResponse(asyncResult),
                (object)null);
            return await task.ContinueWith(t => ReadStreamFromResponse(t.Result));
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using(Stream responseStream = response.GetResponseStream())
            using(StreamReader sr = new StreamReader(responseStream))
            {
                string strContent = sr.ReadToEnd();
                return strContent;
            }
        }

        private void FinishWebRequest(IAsyncResult result)
        {
            HttpWebResponse response = (result.AsyncState as HttpWebRequest).EndGetResponse(result) as HttpWebResponse;
            StreamReader read = new StreamReader(response.GetResponseStream());
            string output = read.ReadToEnd();
        }

        private HttpWebRequest PrepareRequest(string resourcePath, Dictionary<string,string> options, Dictionary<string,string> urlSegments = null)
        {
            options = options ?? new Dictionary<string,string>();
            urlSegments = urlSegments ?? new Dictionary<string,string>();
            string requestUrl = BaseUrl.ToString() + resourcePath + "?";
            TimeSpan timestamp = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            var hash = GetHash(timestamp + _privateKey + _publicKey);
            foreach(KeyValuePair<string, string> o in options)
            {
                requestUrl = requestUrl + o.Key + "=" + o.Value + "&";
            }

            foreach(KeyValuePair<string,string> segment in urlSegments)
            {
                requestUrl = requestUrl + segment.Key + "=" + segment.Value + "&";
            }

            requestUrl = requestUrl + "ts=" + timestamp + "&";
            requestUrl = requestUrl + "apikey=" + _publicKey + "&";
            requestUrl = requestUrl + "hash=" + hash;
            
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Accept = "application/json";
            request.Method = "GET";
            request.Proxy = null;
            return request;
        }

        private string GetHash(string timestamp)
        {
            var combined = timestamp + _privateKey + _publicKey;
            var bytes = Encoding.UTF8.GetBytes(combined);

            //Create object that allows the md5 algorithm to be used
            var md5 = Windows.Security.Cryptography.Core.HashAlgorithmProvider.OpenAlgorithm(Windows.Security.Cryptography.Core.HashAlgorithmNames.Md5);
            var vector = CryptographicBuffer.ConvertStringToBinary(timestamp, BinaryStringEncoding.Utf8);
            var digest = md5.HashData(vector);
            var hash = md5.CreateHash();
            string converted = CryptographicBuffer.EncodeToHexString(digest);

            return converted;
        }

        private static Dictionary<string,string> UrlSegmentFor(string segmentName, object segmentValue)
        {
            return new Dictionary<string,string> { { segmentName, segmentValue.ToString() } };
        }

        protected abstract string Resource { get; }
        public Func<string> CreateRequestClient { get; internal set; }
    }
}