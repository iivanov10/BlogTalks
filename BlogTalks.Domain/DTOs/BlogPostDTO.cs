namespace BlogTalks.Domain.DTOs
{
    public class BlogPostDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Timestamp {  get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
