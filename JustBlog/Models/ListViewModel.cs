using System;
using JustBlog.Core.Objects;
using JustBlog.Core.API;
using System.Collections.Generic;

namespace JustBlog.Models
{
    public class ListViewModel
    {
        public IList<Post> Posts { get;  set; }
        public int TotalPosts { get; set; }

        public ListViewModel(IBlogRepository _blogRepository, int p)
        {
            Posts = _blogRepository.Posts(p - 1, 10);
            TotalPosts = _blogRepository.TotalPosts();
        }
    }
}