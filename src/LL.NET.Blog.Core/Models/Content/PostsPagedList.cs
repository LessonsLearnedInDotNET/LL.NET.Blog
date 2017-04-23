using System.Collections.Generic;

namespace LL.NET.Blog.Core.Models.Content
{
    public class PostsPagedList
    {
        public IEnumerable<Post> Posts { get; private set; }
        public int TotalResults { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public PostsPagedList(IEnumerable<Post> posts, int totalResults, int currentPage, int pageSize)
        {
            Posts = posts;
            TotalResults = totalResults;
            CurrentPage = currentPage;
            PageSize = pageSize;
            TotalPages = ((int)(TotalResults / PageSize)) + ((TotalResults % PageSize) > 0 ? 1 : 0);
        }
    }
}
