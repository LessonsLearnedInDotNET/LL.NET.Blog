using Microsoft.AspNetCore.Mvc;

namespace LL.NET.Blog.Web.Controllers
{
    [Route("about")]
    public class AboutController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.PageHeaderTitle = "Larry Burris";
            ViewBag.PageHeaderSubtitle = "A Student and a Teacher";
            return View();
        }
    }
}
