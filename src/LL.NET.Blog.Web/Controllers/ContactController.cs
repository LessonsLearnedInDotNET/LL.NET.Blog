using LL.NET.Blog.Core.Services;
using LL.NET.Blog.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace LL.NET.Blog.Web.Controllers
{
    [Route("contact")]
    public class ContactController : Controller
    {
        private IMailService _mailService;
        private ILogger<ContactController> _logger;

        public ContactController(IMailService mailService, ILogger<ContactController> logger)
        {
            _mailService = mailService;
            _logger = logger;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.PageHeaderTitle = "Send Me A Message";
            ViewBag.PageHeaderSubtitle = "All feedback is greatly appreciated!";
            return View();
        }

        [HttpPost("")]
        public async Task<IActionResult> Index([FromForm]ContactModel model)
        {
            if (!ModelState.IsValid)
                return View();

            try
            {
                    await _mailService.SendMail("ContactTemplate.txt", model.Name, model.Email, model.Subject, model.Message);
                    return Ok(new { Success = true, Message = "Message Sent" });
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to send email from contact page", ex);
                return BadRequest(new { Reason = "Error Occurred" });
            }
        }
    }
}
