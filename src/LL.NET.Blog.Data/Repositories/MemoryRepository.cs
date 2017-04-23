using LL.NET.Blog.Core.Models.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LL.NET.Blog.Data.Repositories
{
    public class MemoryRepository : IBlogRepository
    {
        public void AddPost(Post post)
        {
            post.Id = _posts.Max(s => s.Id) + 1;
            _posts.Add(post);
        }

        public bool DeletePost(int postId)
        {
            var post = GetPost(postId);
            if (post != null)
            {
                _posts.Remove(post);
            }

            return post != null;
        }

        public IEnumerable<string> GetTags()
        {
            return _posts.SelectMany(p => p.Tags.Select(t => t.Value))
                         .Where(t => !string.IsNullOrWhiteSpace(t))
                         .OrderBy(t => t)
                         .ToList();
        }

        public PostsPagedList GetPosts(int pageSize = 10, int page = 1)
        {
            return new PostsPagedList(_posts.Skip(GetSkipCount(page, pageSize)).Take(pageSize), _posts.Count(), page, pageSize);
        }

        public PostsPagedList GetPostsByTerm(string term, int pageSize = 10, int page = 1)
        {
            var totalCount = GetPostsBySearchTerm(term).Count();
            var matchingPosts = GetPostsBySearchTerm(term).Skip(GetSkipCount(page, pageSize)).Take(pageSize);
            return new PostsPagedList(matchingPosts, totalCount, page, pageSize);
        }

        public PostsPagedList GetPostsByTag(string tag, int pageSize = 10, int page = 1)
        {
            var lowerTag = tag.ToLowerInvariant();
            var totalCount = GetPostsByTag(lowerTag).Count();
            var matchingPosts = GetPostsByTag(lowerTag).Skip(GetSkipCount(page, pageSize)).Take(pageSize);
            return new PostsPagedList(matchingPosts, totalCount, page, pageSize);
        }

        public Post GetPost(string slug)
        {
            return _posts.Where(s => s.Slug == slug).FirstOrDefault();
        }

        public Post GetPost(int id)
        {
            return _posts.Where(s => s.Id == id).FirstOrDefault();
        }

        public void SaveAll()
        {
        }

        private IEnumerable<Post> GetPostsByTag(string lowerTag)
        {
            return _posts.Where(s => s.Tags.Any(t => t.Value.ToLowerInvariant().Contains(lowerTag)));
        }

        private IEnumerable<Post> GetPostsBySearchTerm(string searchTerm)
        {
            var lowerTerm = searchTerm.ToLowerInvariant();
            return _posts.Where(p => p.Body.ToLowerInvariant().Contains(lowerTerm)
                             || p.Tags.Any(t => t.Value.ToLowerInvariant().Contains(lowerTerm))
                             || p.Title.ToLowerInvariant().Contains(lowerTerm));
        }

        private int GetSkipCount(int page, int pageSize)
        {
            return (page - 1) * pageSize;
        }

        static List<Post> _posts = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                IsPublished = true,
                Title = "Launching Default ASP.NET Core MVC Project",
                Slug = "2017/3/1/Launching_Default_ASP_NET_Core_MVC_Project",
                Tags = new List<Tag> {
                    new Tag { Id = 1, Value = "ASP.NET Core" },
                    new Tag { Id = 2, Value = "MVC" }
                },
                DatePublished = DateTime.UtcNow,
                Body = @"<p><img style=""float: right; display: inline"" src=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" width=""200"" align=""right"" height=""200"">Let's see what the basic ASP.NET Core MVC default project looks like</p> <p>ASP.NET Core come with an awesome feature set built in. Many of the common patterns that the .NET community has typically used libraries for can be found as built in libraries.</p> <p>For example, let's see how dependency injection works for ASP.NET Core.</p> <p>Next, let's see how the MVC TagHelpers library has been adopted.</p>"
            },
            new Post()
            {
                Id = 2,
                IsPublished = true,
                Title = "Angular 2 and ASP.NET Core",
                Slug = "2017/3/14/Angular_2_and_ASP_NET_Core",
                Tags = new List<Tag> {
                    new Tag { Id = 1, Value = "ASP.NET Core" },
                    new Tag { Id = 3, Value = "Angular2" }
                },
                DatePublished = DateTime.UtcNow,
                Body = @"<p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200""><img title=""image"" style=""float: right; display: inline"" alt=""image"" src=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" width=""234"" align=""right"" height=""161""></a>An example where we've used Angular 2 working in a simple ASP.NET Core app. There are some things to know about getting Angular working in .NET Core environments.</p> <p>Angular, the team has transitioned to not using suffix such as JS, or 2, is already getting popular in the developer community. Let's see how to build an Angular 2 app in an ASP.NET Core application.</p><p>Now that you've seen how well Angular and ASP.NET Core work together, try it out yourself!</p> <blockquote> <p><a title=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"">https://avatars2.githubusercontent.com/u/20156516?v=3&s=200</a></p></blockquote><p>Tell me what you think!</p><p><pre>c:/>dnx web</pre></p><p></p>"
            },
            new Post()
            {
                Id = 3,
                IsPublished = true,
                Title = "ASP.NET Core Web API on Docker",
                Slug = "2017/4/3/ASP_NET_Core_MVC_on_Docker",
                Tags = new List<Tag> {
                    new Tag { Id = 1, Value = "ASP.NET Core" },
                    new Tag { Id = 4, Value = "Web API" },
                    new Tag { Id = 5, Value = "Docker" }
                },
                DatePublished = DateTime.UtcNow,
                Body = @"<p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200""><img title=""Print"" style=""border-top: 0px; border-right: 0px; background-image: none; border-bottom: 0px; float: right; padding-top: 0px; padding-left: 0px; border-left: 0px; display: inline; padding-right: 0px"" border=""0"" alt=""Print"" src=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" width=""240"" align=""right"" height=""81""></a>In this example, we cover hosting an ASP.NET Core Web API instance inside of a docker container.</p> <p>Docker containers can be really convenient when it comes to handling devops.</p> <h3>Web API Design</h3> <blockquote> <p><a href=""http://wildermuth.com/downloads/devintersection-fall2015-APIDesign.pdf"" target=""_blank"">Slides</a></p></blockquote> <h3>Looking Ahead to Bootstrap 4</h3> <blockquote> <p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" target=""_blank"">Slides</a></p> <p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" target=""_blank"">Demos</a></p></blockquote> <p> <h3>Entities or&nbsp; View Models in ASP.NET Development</h3></p> <blockquote> <p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" target=""_blank"">Slides</a></p> <p><a href=""https://avatars2.githubusercontent.com/u/20156516?v=3&s=200"" target=""_blank"">Demos</a></p></blockquote> <p>Be sure to comment below and tell me how your process expands upon this.</p>"
            },
        };
    }
}
