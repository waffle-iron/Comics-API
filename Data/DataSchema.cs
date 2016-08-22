using System;

namespace ComicsAPI.Data
{
    public class Rootobject
    {
        public int code { get; set; }
        public string status { get; set; }
        public string copyright { get; set; }
        public string attributionText { get; set; }
        public string attributionHTML { get; set; }
        public string etag { get; set; }
        public Data data { get; set; }
    }

    public class Data
    {
        public int offset { get; set; }
        public int limit { get; set; }
        public int total { get; set; }
        public int count { get; set; }
        public Result[] results { get; set; }
    }

    public class Result
    {
        public string title { get; set; }
        public string issueNumber { get; set; }
        public string variantDescription { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public DateTime modified { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string resourceURI { get; set; }
        public Comics comics { get; set; }
        public Series series { get; set; }
        public Events events { get; set; }
        public Url[] urls { get; set; }
        public Creators creators { get; set; }
        public Characters characters { get; set; }
        public Price[] prices { get; set; }
        public Date[] dates { get; set; }
        public int startYear { get; set; }
        public int endYear { get; set; }
        public string rating { get; set; }
        public string type { get; set; }
        public object next { get; set; }
        public object previous { get; set; }
    }

    public class Thumbnail
    {
        public string path { get; set; }
        public string extension { get; set; }
    }

    public class Comics
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public Item[] items { get; set; }
        public int returned { get; set; }
    }

    public class Item
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Series
    {
        public int available { get; set; }
        public string name { get; set; }
        public string resourceURI { get; set; }
        public Item1[] items { get; set; }
        public int returned { get; set; }
    }

    public class Item1
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Stories
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public Item2[] items { get; set; }
        public int returned { get; set; }
    }

    public class Item2
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string type { get; set; }
    }

    public class Events
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public Item3[] items { get; set; }
        public int returned { get; set; }
    }

    public class Item3
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
    }

    public class Url
    {
        public string type { get; set; }
        public string url { get; set; }
    }

    public class Price
    {
        public string type { get; set; }
        public float price { get; set; }
    }

    public class Date
    {
        public string type { get; set; }
        public DateTime date { get; set; }
    }

    public class Characters
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public object[] items { get; set; }
        public int returned { get; set; }
    }

    public class Creators
    {
        public int available { get; set; }
        public string collectionURI { get; set; }
        public Item4[] items { get; set; }
        public int returned { get; set; }
    }

    public class Item4
    {
        public string resourceURI { get; set; }
        public string name { get; set; }
        public string role { get; set; }
    }
};