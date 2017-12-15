using System;
using System.Collections.Generic;
using JustBlog.Core.Objects;

namespace JustBlog.Core.API
{
    public interface IBlogRepository
    {
        IList<Post> Posts(int pageNo, int pageSize);
        int TotalPosts();
    }
}
