using BlogTalks.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Formats.Asn1;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace BlogTalks.Domain.Entities
{
    public class BlogPost : BaseEntity
    {
        public required string Title { get; set; }
        public required string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
