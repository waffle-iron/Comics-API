using ComicsAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using static System.Enum;

namespace ComicsAPI
{
    public enum Publishers
    {
        Marvel,
    }

    public enum ResourceType
    {
        Comic,
        Character,
        Event,
        Creator,
        Series,
    }

    public static class ComicRequests
    {
        public static MarvelAPI.MarvelClient marvelClient = new MarvelAPI.MarvelClient("269d5235a5258dd2df9dcae4965f3dda", "34dd8b34c0b2975ee2883d6af3053fe577f157f3");

        public static Task<Rootobject> getThisWeekComic(Publishers pub)
        {
            switch (pub)
            {
                case Publishers.Marvel:
                    return marvelClient.Comics.FindAll(20, 0, new Dictionary<string, string> { { "format", "comic" }, { "formatType", "comic" }, { "dateDescriptor", "thisWeek" }, { "orderBy", "onsaleDate" }, { "noVariants", "true" } });
            }
            throw new System.Exception("Invalid publisher");
        }

        public static Task<Rootobject> getNextWeekComic(Publishers pub)
        {
            switch (pub)
            {
                case Publishers.Marvel:
                    return marvelClient.Comics.FindAll(20, 0, new Dictionary<string, string> { { "format", "comic" }, { "formatType", "comic" }, { "dateDescriptor", "nextWeek" }, { "orderBy", "onsaleDate" }, { "noVariants", "true" } });
            }
            throw new System.Exception("Invalid publisher");
        }

        public static Task<Rootobject> getCurrentEvent(Publishers pub)
        {
            switch (pub)
            {
                case Publishers.Marvel:
                    return marvelClient.Events.FindAll(20, 0, new Dictionary<string, string> { { "orderBy", "-startDate" } });
            }
            throw new System.Exception("Invalid publisher");
        }

        public static Task<Rootobject> searchAll(string searchString, ResourceType type, int offset, int limit)
        {
            switch (type)
            {
                case ResourceType.Character:
                    return marvelClient.Characters.FindAll(limit, offset, new Dictionary<string, string> { { "nameStartsWith", searchString } });
                case ResourceType.Comic:
                    return marvelClient.Comics.FindAll(limit, offset, new Dictionary<string, string> { { "titleStartsWith", searchString } });
                case ResourceType.Creator:
                    return marvelClient.Creators.FindAll(limit, offset, new Dictionary<string, string> { { "nameStartsWith", searchString } });
                case ResourceType.Event:
                    return marvelClient.Events.FindAll(limit, offset, new Dictionary<string, string> { { "nameStartsWith", searchString } });
                case ResourceType.Series:
                    return marvelClient.Series.FindAll(limit, offset, new Dictionary<string, string> { { "titleStartsWith", searchString } });
                default:
                    throw new System.ArgumentException("That resource type is not supported!");
            }
        }

        public static Task<Rootobject>[] SearchAllResources(string searchString)
        {
            Task<Rootobject>[] result = new Task<Rootobject>[GetNames(typeof(ResourceType)).Length];

            for(int i = 0; i < result.Length; i++)
                result[i] = searchAll(searchString, (ResourceType)i, 0, 20);

            return result;
        }
    }
}
