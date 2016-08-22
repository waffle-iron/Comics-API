using System;
namespace MarvelAPI
{
    public class MarvelClient
    {
        private readonly string _publicKey;
        private readonly string _privateKey;
        public MarvelClient(string publicKey, string privateKey)
        {
            if (string.IsNullOrEmpty(publicKey)) throw new ArgumentException("You must supply a public API key.");
            if (string.IsNullOrEmpty(privateKey)) throw new ArgumentException("You must supply a private API key.");
            _publicKey = publicKey;
            _privateKey = privateKey;
        }

        public CharactersClient Characters
        {
            get
            {
                return ConfigureClient(new CharactersClient(_publicKey, _privateKey));
            }
        }
        public ComicsClient Comics
        {
            get
            {
                return ConfigureClient(new ComicsClient(_publicKey, _privateKey));
            }
        }
        public CreatorsClient Creators
        {
            get
            {
                return ConfigureClient(new CreatorsClient(_publicKey, _privateKey));
            }
        }
        public EventsClient Events
        {
            get
            {
                return ConfigureClient(new EventsClient(_publicKey, _privateKey));
            }
        }
        public SeriesClient Series
        {
            get
            {
                return ConfigureClient(new SeriesClient(_publicKey, _privateKey));
            }
        }
        public StoriesClient Stories
        {
            get
            {
                return ConfigureClient(new StoriesClient(_publicKey, _privateKey));
            }
        }
        private T ConfigureClient<T>(T client) where T : ClientBase
        {
            if (CreateRequestClient != null)
            {
                client.CreateRequestClient = CreateRequestClient;
            }
            return client;
        }
        public Func<string> CreateRequestClient { get; set; }
    }
}