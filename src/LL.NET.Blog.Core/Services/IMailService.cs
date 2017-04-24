using System.Threading.Tasks;

namespace LL.NET.Blog.Core.Services
{
    public interface IMailService
    {
        Task SendMail(string template, string name, string email, string subject, string message);
    }
}
