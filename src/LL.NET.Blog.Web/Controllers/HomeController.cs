using LL.NET.Blog.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LL.NET.Blog.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private IBlogRepository _repo;
        private ILogger<HomeController> _logger;
        readonly int _pageSize = 25;

        public HomeController(IBlogRepository repo, ILogger<HomeController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return GetPostsPagedList(1);
        }

        [HttpGet("blog/{page:int?}")]
        public IActionResult GetPostsPagedList(int page)
        {
            return View("Index", _repo.GetPosts(_pageSize, page));
        }

        [HttpGet("{year:int}/{month:int}/{day:int}/{slug}")]
        public IActionResult Post(int year, int month, int day, string slug)
        {
            var fullSlug = $"{year}/{month}/{day}/{slug}";

            try
            {
                var story = _repo.GetPost(fullSlug);
                if (story == null)
                    story = _repo.GetPost($"{year:0000}/{month:00}/{day:00}/{slug}");
                if (story != null)
                    return View(story);
            }
            catch
            {
                _logger.LogWarning($"Couldn't find the ${fullSlug} story");
            }

            return Redirect("/");
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
