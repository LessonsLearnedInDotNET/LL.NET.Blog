using System.ComponentModel.DataAnnotations;

namespace LL.NET.Blog.Web.Models
{
    public class ContactModel
    {
        [Required]
        [StringLength(256, MinimumLength = 5)]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Subject { get; set; }
    }
}
