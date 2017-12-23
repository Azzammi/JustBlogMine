using System;

using JustBlog.Core.Objects;
using JustBlog.Core.API;

using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using NHibernate.Transform;
using System.Collections.Generic;
using System.Linq;

namespace JustBlog.Core.Repository
{
    public class BlogRepository : IBlogRepository
    {
        //NHibernate Object
        private readonly ISession _session;

        #region Constructor
        public BlogRepository(ISession session)
        {
            _session = session;
        }
        #endregion

        #region Show All data
        public IList<Post> Posts(int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>().Where(p => p.Published)
                                              .OrderByDescending(p => p.PostedOn)
                                              .Skip(pageNo * pageSize)
                                              .Take(pageSize)
                                              .Fetch(p => p.Category)
                                              .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                .Where(p => postIds.Contains(p.Id))
                .OrderByDescending(p => p.PostedOn)
                .FetchMany(p => p.Tags)
                .ToList();
        }

        public int TotalPosts()
        {
            return _session.Query<Post>().Where(p => p.Published).Count();
        }

        #endregion

        #region Show By Category
        public IList<Post> PostsForCategory(string categorySlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                        .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                        .OrderByDescending(p => p.PostedOn)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Fetch(p => p.Category)
                        .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                            .Where(p => postIds.Contains(p.Id))
                            .OrderByDescending(p => p.PostedOn)
                            .FetchMany(p => p.Tags)
                            .ToList();
        }

        public int TotalPostsForCategory(string categorySlug)
        {
            return _session.Query<Post>()
                        .Where(p => p.Published && p.Category.UrlSlug.Equals(categorySlug))
                        .Count();
        }

        public Category Category(string categorySlug)
        {
            return _session.Query<Category>()
                        .FirstOrDefault(t => t.UrlSlug.Equals(categorySlug));
        }
        #endregion

        #region Show By Slug
        public IList<Post> PostsForTag(string tagSlug, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                        .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                        .OrderByDescending(p => p.PostedOn)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Fetch(p => p.Category)
                        .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                    .Where(p => postIds.Contains(p.Id))
                    .OrderByDescending(p => p.PostedOn)
                    .FetchMany(p => p.Tags)
                    .ToList();
        }
        public int TotalPostsForTag(string tagSlug)
        {
            return _session.Query<Post>()
                            .Where(p => p.Published && p.Tags.Any(t => t.UrlSlug.Equals(tagSlug)))
                            .Count();
        }
        public Tag Tag(string tagSlug)
        {
            return _session.Query<Tag>()
                    .FirstOrDefault(t => t.UrlSlug.Equals(tagSlug));
        }
        #endregion

        #region Search Post
        public IList<Post> PostsForSearch(string search, int pageNo, int pageSize)
        {
            var posts = _session.Query<Post>()
                        .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                        .OrderByDescending(p => p.PostedOn)
                        .Skip(pageNo * pageSize)
                        .Take(pageSize)
                        .Fetch(p => p.Category)
                        .ToList();

            var postIds = posts.Select(p => p.Id).ToList();

            return _session.Query<Post>()
                           .Where(p => postIds.Contains(p.Id))
                           .OrderByDescending(p => p.PostedOn)
                           .FetchMany(p => p.Tags)
                           .ToList();
        }

        public int TotalPostsForSearch(string search)
        {
            return _session.Query<Post>()
                .Where(p => p.Published && (p.Title.Contains(search) || p.Category.Name.Equals(search) || p.Tags.Any(t => t.Name.Equals(search))))
                .Count();
        }
        #endregion

        #region Display the post content
        public Post Post(int year, int month, string titleSlug)
        {
            var query = _session.Query<Post>()
                                .Where(p => p.PostedOn.Year == year && p.PostedOn.Month == month && p.UrlSlug.Equals(titleSlug))
                                .Fetch(p => p.Category);

            query.FetchMany(p => p.Tags).ToFuture();

            return query.ToFuture().Single();
        }
        #endregion

        #region Display The Category Widget
        public IList<Category> Categories()
        {
            return _session.Query<Category>().OrderBy(p => p.Name).ToList();
        }
        #endregion
    }
}
