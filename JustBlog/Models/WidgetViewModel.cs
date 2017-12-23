using System.Collections.Generic;
using JustBlog.Core;
using JustBlog.Core.Objects;
using JustBlog.Core.API;

namespace JustBlog.Models
{
    public class WidgetViewModel
    {
        public IList<Category> Categories { get; set; }

        public WidgetViewModel(IBlogRepository blogRepository)
        {
            Categories = blogRepository.Categories();
        }
    }
}