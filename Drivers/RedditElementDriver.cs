using Orchard.Layouts.Framework.Display;
using Orchard.Layouts.Framework.Drivers;
using Ouwinga.Tutorials.Elements;
using Ouwinga.Tutorials.Services;
using Ouwinga.Tutorials.ViewModels;
using System.Linq;

namespace Ouwinga.Tutorials.Drivers
{
    public class RedditElementDriver : ElementDriver<Reddit>
    {
        private readonly IRedditService _redditService;

        public RedditElementDriver(IRedditService redditService)
        {
            _redditService = redditService;
        }

        protected override EditorResult OnBuildEditor(Reddit element, ElementEditorContext context)
        {
            var viewModel = new RedditEditorViewModel
            {
                Subreddit = element.Subreddit,
                CacheMinutes = element.CacheMinutes
            };

            var editor = context.ShapeFactory.EditorTemplate(TemplateName: "Elements.Reddit", Model: viewModel);

            if (context.Updater != null)
            {
                context.Updater.TryUpdateModel(viewModel, context.Prefix, null, null);
                element.Subreddit = viewModel.Subreddit;
                element.CacheMinutes = viewModel.CacheMinutes;
            }

            return Editor(context, editor);
        }

        protected override void OnDisplaying(Reddit element, ElementDisplayContext context)
        {
            context.ElementShape.Posts = _redditService.GetPosts(element.Subreddit, element.CacheMinutes).ToList();
        }
    }
}
