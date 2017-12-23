using System;
using System.Collections.Generic;
using JustBlog.Core.Objects;

namespace JustBlog.Core.API
{
    public interface IBlogRepository
    {
        // For Showing all the posts
        IList<Post> Posts(int pageNo, int pageSize);
        int TotalPosts();

        // For showing the detail of the post based on category
        IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize);
        int TotalPostsForCategory(string categorySlug);
        Category Category(string categorySlug);

        // For showing data based on Slug
        IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize);
        int TotalPostsForTag(string tagSlug);
        Tag Tag(string tagSlug);

        // Search method
        IList<Post> PostsForSearch(string search, int pageNo, int pageSize);
        int TotalPostsForSearch(string search);

        // Display the detail of the single post
        Post Post(int year, int month, string titleSlug);

        //Display the Category widget
        IList<Category> Categories();
    }
}
