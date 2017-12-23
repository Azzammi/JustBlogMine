using System;
using JustBlog.Core.Objects;
using JustBlog.Core.API;
using System.Collections.Generic;

namespace JustBlog.Models
{
    public class ListViewModel
    {
        #region Properties
        public IList<Post> Posts { get; set; }
        public int TotalPosts { get; set; }
        public Category Category { get; set; }
        public Tag Tag { get; set; }
        public string Search { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// To Show All Posts
        /// </summary>
        /// <param name="_blogRepository"></param>
        /// <param name="p"></param>
        public ListViewModel(IBlogRepository _blogRepository, int p)
        {
            Posts = _blogRepository.Posts(p - 1, 10);
            TotalPosts = _blogRepository.TotalPosts();
        }

        /// <summary>
        /// To Show posts based on category or tag
        /// </summary>
        /// <param name="blogRepository"></param>
        /// <param name="text"></param>
        /// <param name="type"></param>
        /// <param name="p"></param>
        public ListViewModel(IBlogRepository blogRepository, string text, string type, int p)
        {
            switch (type)
            {
                case "Category":
                    Posts = blogRepository.PostsForCategory(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForCategory(text);
                    Category = blogRepository.Category(text);
                    break;
                case "Tag":
                    Posts = blogRepository.PostsForTag(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForTag(text);
                    Tag = blogRepository.Tag(text);
                    break;
                default:
                    Posts = blogRepository.PostsForSearch(text, p - 1, 10);
                    TotalPosts = blogRepository.TotalPostsForSearch(text);
                    Search = text;
                    break;
            }
        }
        #endregion
    }
}