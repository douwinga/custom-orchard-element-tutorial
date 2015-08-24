using Newtonsoft.Json;

namespace Ouwinga.Tutorials.Models
{

    public class RedditJson
    {
        [JsonProperty("data")]
        public RedditDataJson Data { get; set; }
    }

    public class RedditDataJson
    {
        [JsonProperty("children")]
        public RedditPostJson[] Children { get; set; }
    }

    public class RedditPostJson
    {
        [JsonProperty("data")]
        public RedditPostDataJson Data { get; set; }
    }

    public class RedditPostDataJson
    {
        [JsonProperty("thumbnail")]
        public string ThumbnailUrl { get; set; }
        [JsonProperty("url")]
        public string LinkUrl { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
    }
}