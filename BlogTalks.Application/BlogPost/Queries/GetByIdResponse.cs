using BlogTalks.Domain.DTOs;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetByIdResponse
    {
        public required string Title { get; set; }
        public required string Text { get; set; }
        public int CreatedBy { get; set; }
        public DateTime Timestamp { get; set; }
        public List<string> Tags { get; set; } = new List<string>();
        public List<CommentDTO> Comments { get; set; } = new List<CommentDTO>();
    }
}
