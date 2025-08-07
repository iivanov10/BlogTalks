using BlogTalks.Domain.Shared;

namespace BlogTalks.Domain.Entities
{
    public class Comment : IEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; } = new BlogPost();
    }
}
