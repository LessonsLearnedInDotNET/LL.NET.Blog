using LL.NET.Blog.Core.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace LL.NET.Blog.Web.Services.Mail
{
    public class SendGridMailService : IMailService
    {
        private IConfigurationRoot _config;
        private IHostingEnvironment _env;
        private ILogger<SendGridMailService> _logger;

        const string Uri = "https://api.sendgrid.com/api/mail.send.json";

        public SendGridMailService(IHostingEnvironment env, IConfigurationRoot config, ILogger<SendGridMailService> logger)
        {
            _config = config;
            _env = env;
            _logger = logger;
        }

        public async Task SendMail(string template, string name, string email, string subject, string message)
        {
            var bodyTemplate = GetTemplate(template);
            var body = string.Format(bodyTemplate, email, name, subject, message);
            var content = new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("api_user", _config["MailService:ApiUser"]),
                new KeyValuePair<string, string>("api_key", _config["MailService:ApiKey"]),
                new KeyValuePair<string, string>("to", _config["MailService:Receiver"]),
                new KeyValuePair<string, string>("toname", name),
                new KeyValuePair<string, string>("subject", $"LessonsLearnedIn.NET Site Mail: {subject}"),
                new KeyValuePair<string, string>("text", body),
                new KeyValuePair<string, string>("from", _config["MailService:Receiver"])
            };

            try
            {
                await PostToSendGrid(content);
            }
            catch(Exception ex)
            {
                _logger.LogError("Exception Thrown sending message via SendGrid", ex);
            }
        }

        private string GetTemplate(string template)
        {
            var path = $"{_env.ContentRootPath}\\EmailTemplates\\{template}";
            return File.ReadAllText(path);
        }

        private async Task PostToSendGrid(KeyValuePair<string, string>[] requestContent)
        {
            var client = new HttpClient();
            var response = await client.PostAsync(Uri, new FormUrlEncodedContent(requestContent));
            if (!response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Failed to send message via SendGrid: {Environment.NewLine}Body: {requestContent}{Environment.NewLine}Result: {result}");
            }
        }
    }
}
