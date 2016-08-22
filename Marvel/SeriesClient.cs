using System.Collections.Generic;
using System.Threading.Tasks;


namespace MarvelAPI
{
    public class SeriesClient : ClientBase
    {
        internal SeriesClient(string publicKey, string privateKey)
            : base(publicKey, privateKey)
        { }
        public dynamic Characters(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, CharactersResource, limit, offset, queryParameters);
        }
        public dynamic Comics(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, ComicsResource, limit, offset, queryParameters);
        }
        public dynamic Creators(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, CreatorsResource, limit, offset, queryParameters);
        }
        public dynamic Events(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, EventsResource, limit, offset, queryParameters);
        }
        public dynamic Stories(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, StoriesResource, limit, offset, queryParameters);
        }
        protected override string Resource
        {
            get { return "series"; }
        }
    }
}