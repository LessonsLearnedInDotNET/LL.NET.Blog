using System;
using Microsoft.Extensions.Logging;
using LL.NET.Blog.Core.Services;

namespace LL.NET.Blog.Web.Services.Logger
{
    public class EmailLogger : ILogger
    {
        private string _categoryName;
        private Func<string, LogLevel, bool> _filter;
        private IMailService _mailService;

        public EmailLogger(string categoryName, Func<string, LogLevel, bool> filter, IMailService mailService)
        {
            _categoryName = categoryName;
            _filter = filter;
            _mailService = mailService;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return (_filter == null || _filter(_categoryName, logLevel));
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }

            if (formatter == null)
            {
                throw new ArgumentNullException(nameof(formatter));
            }

            var message = formatter(state, exception);

            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            message = $@"Level: {logLevel} {message}";

            if (exception != null)
            {
                message += Environment.NewLine + Environment.NewLine + exception.ToString();
            }

            _mailService.SendMail("LogMessage.txt", "Larry Burris", "larry@lessonslearnedin.net", "[LL.NET.Blog Log Message]", message);
        }
    }
}