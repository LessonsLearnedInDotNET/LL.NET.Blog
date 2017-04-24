using System.Threading.Tasks;
using LL.NET.Blog.Core.Services;
using Microsoft.Extensions.Logging;

namespace LL.NET.Blog.Web.Services.Mail
{
    public class LoggingMailService : IMailService
    {
        private ILogger<LoggingMailService> _logger;

        public LoggingMailService(ILogger<LoggingMailService> logger)
        {
            _logger = logger;
        }

        public Task SendMail(string template, string name, string email, string subject, string message)
        {
            _logger.LogDebug($"Email Requested from {name} subject of {subject}");
            return Task.Delay(0);
        }
    }
}
