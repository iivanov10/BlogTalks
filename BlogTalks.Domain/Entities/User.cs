using BlogTalks.Domain.Shared;

namespace BlogTalks.Domain.Entities
{
    public class User : BaseEntity
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public List<BlogPost> BlogPosts { get; set; } = new List<BlogPost>();
    }
}
