using BlogTalks.Domain.Shared;

namespace BlogTalks.Domain.Entities
{
    public class Tag : BaseEntity
    {
        public required string Name { get; set; }
    }
}
