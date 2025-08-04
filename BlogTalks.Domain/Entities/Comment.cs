using BlogTalks.Domain.Shared;

namespace BlogTalks.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public required string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public int BlogPostId { get; set; }
        public required BlogPost BlogPost { get; set; }
    }
}
