using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreTest.Models.Entities
{
    public class Article : Entity
    {
        public string Tag { get; set; }

        public string Content { get; set; }

        public DateTime DateAvailable { get; set; }

        public string Title { get; set; }

        public string Subtitle { get; set; }

        public string Author { get; set; }

        public ArticleType ArticleType { get; set; }

        //public Type LinkImage { get; set; }
    }

    public enum ArticleType
    {
        Video,
        Text
    }
}
