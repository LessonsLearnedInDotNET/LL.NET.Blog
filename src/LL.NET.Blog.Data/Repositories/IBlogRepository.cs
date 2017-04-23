using LL.NET.Blog.Core.Models.Content;
using System.Collections.Generic;

namespace LL.NET.Blog.Data.Repositories
{
    public interface IBlogRepository
    {
        PostsPagedList GetPosts(int pageSize = 10, int page = 1);
        PostsPagedList GetPostsByTerm(string term, int pageSize, int page);
        PostsPagedList GetPostsByTag(string tag, int pageSize, int page);

        Post GetPost(int id);
        Post GetPost(string slug);
        void AddPost(Post story);

        void SaveAll();
        bool DeletePost(int postId);

        IEnumerable<string> GetTags();
    }
}
