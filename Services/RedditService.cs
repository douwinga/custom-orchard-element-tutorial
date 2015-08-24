using Newtonsoft.Json;
using Orchard.Caching;
using Orchard.Services;
using Ouwinga.Tutorials.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Ouwinga.Tutorials.Services
{
    public class RedditService : IRedditService
    {
        protected readonly string _cacheKeyPrefix = "C4B4FC42-56F3-43AB-972F-1ADF0F271D01_";
        private readonly ICacheManager _cacheManager;
        private readonly IClock _clock;

        public RedditService(ICacheManager cacheManager, IClock clock)
        {
            _cacheManager = cacheManager;
            _clock = clock;
        }

        public IEnumerable<RedditPost> GetPosts(string subreddit, int cacheMinutes)
        {
            var cacheKey = _cacheKeyPrefix + subreddit;

            return _cacheManager.Get(cacheKey, ctx =>
            {
                ctx.Monitor(_clock.When(TimeSpan.FromMinutes(cacheMinutes)));
                return RetrieveRedditPosts(subreddit);
            });
        }

        private IEnumerable<RedditPost> RetrieveRedditPosts(string subreddit)
        {
            // Default to grabbing frontpage posts
            var apiUrl = "https://www.reddit.com/.json";

            if (!string.IsNullOrWhiteSpace(subreddit))
            {
                apiUrl = "https://www.reddit.com/r/" + subreddit + "/.json";
            }

            var request = (HttpWebRequest)WebRequest.Create(apiUrl);
            request.Method = "GET";

            var response = request.GetResponse();

            var responseStream = response.GetResponseStream();

            if (responseStream == null)
            {
                return null;
            }

            using (var reader = new StreamReader(responseStream))
            {
                var redditJson = JsonConvert.DeserializeObject<RedditJson>(reader.ReadToEnd());

                responseStream.Close();

                return RedditJsonToRedditPosts(redditJson);
            }
        }

        private IEnumerable<RedditPost> RedditJsonToRedditPosts(RedditJson redditJson)
        {
            var redditPosts = new List<RedditPost>();

            if (redditJson == null || redditJson.Data == null)
            {
                return null;
            }

            foreach (var child in redditJson.Data.Children)
            {
                if (child.Data == null)
                {
                    continue;
                }

                redditPosts.Add(new RedditPost
                {
                    ThumbnailUrl = child.Data.ThumbnailUrl,
                    LinkUrl = child.Data.LinkUrl,
                    Title = child.Data.Title
                });
            }

            return redditPosts;
        }
    }
}