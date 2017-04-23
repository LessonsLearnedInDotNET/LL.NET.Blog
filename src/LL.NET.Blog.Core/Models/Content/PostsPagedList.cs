using System.Collections.Generic;

namespace LL.NET.Blog.Core.Models.Content
{
    public class PostsPagedList
    {
        public IEnumerable<Post> Posts { get; set; }
        public int TotalResults { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
    }
}
