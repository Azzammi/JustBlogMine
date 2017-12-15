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

        public BlogRepository(ISession session)
        {
            _session = session;
        }

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
    }
}
