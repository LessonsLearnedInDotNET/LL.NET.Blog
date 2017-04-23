using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace LL.NET.Blog.Core.Models.Content
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public string Slug { get; set; }
        public List<Tag> Tags { get; set; }
        public DateTime DatePublished { get; set; }
        public bool IsPublished { get; set; }
        public string DisqusId { get; set; }

        public string GetSummary()
        {
            var MAXPARAGRAPHS = 2;
            var regex = new Regex("(<p[^>]*>.*?</p>)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var result = regex.Matches(Body);

            var sb = new StringBuilder();
            var x = 0;
            foreach (Match m in result)
            {
                x++;
                sb.Append(m.Value);
                if (x == MAXPARAGRAPHS)
                    break;
            }
            return sb.ToString();
        }
    }
}
