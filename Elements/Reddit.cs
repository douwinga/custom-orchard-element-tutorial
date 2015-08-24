using Orchard.Layouts.Framework.Elements;
using Orchard.Layouts.Helpers;

namespace Ouwinga.Tutorials.Elements
{
    public class Reddit : Element
    {
        public override string Category
        {
            get { return "Content"; }
        }

        public override bool HasEditor
        {
            get { return true; }
        }

        public string Subreddit
        {
            get { return this.Retrieve(x => x.Subreddit); }
            set { this.Store(x => x.Subreddit, value); }
        }

        public int CacheMinutes
        {
            get { return this.Retrieve(x => x.CacheMinutes); }
            set { this.Store(x => x.CacheMinutes, value); }
        }
    }
}
