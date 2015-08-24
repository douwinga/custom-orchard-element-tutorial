using Orchard;
using Ouwinga.Tutorials.Models;
using System.Collections.Generic;

namespace Ouwinga.Tutorials.Services
{
    public interface IRedditService : IDependency
    {
        IEnumerable<RedditPost> GetPosts(string subreddit, int cacheMinutes);
    }
}