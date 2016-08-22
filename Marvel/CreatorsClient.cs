using System.Collections.Generic;
using System.Threading.Tasks;

namespace MarvelAPI
{
    public class CreatorsClient : ClientBase
    {
        internal CreatorsClient(string publicKey, string privateKey)
            : base(publicKey, privateKey)
        { }

        public dynamic Comics(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, ComicsResource, limit, offset, queryParameters);
        }
        public dynamic Events(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, EventsResource, limit, offset, queryParameters);
        }
        public dynamic Series(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, SeriesResource, limit, offset, queryParameters);
        }
        public dynamic Stories(int id, int limit = 20, int offset = 0, Dictionary<string,string> queryParameters = null)
        {
            return QueryIdSubPath(id, StoriesResource, limit, offset, queryParameters);
        }

        protected override string Resource
        {
            get { return "creators"; }
        }
    }
}