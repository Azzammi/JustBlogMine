using System.Collections.Generic;
using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Core.API;

namespace JustBlog.Models
{
    public class WidgetViewModel
    {
        public IList<Category> Categories { get; private set; }
        public IList<Tag> Tags { get; private set; }
        public IList<Post> LatestPosts { get; private set; }

        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
            Tags = blogRepository.Tags();
            LatestPosts = blogRepository.Posts(0, 10);
        }


    }
}