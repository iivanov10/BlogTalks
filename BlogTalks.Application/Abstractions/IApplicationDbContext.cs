using Microsoft.EntityFrameworkCore;

namespace BlogTalks.Application.Abstractions
{
    public interface IApplicationDbContext
    {
        public DbSet<Domain.Entities.BlogPost> BlogPosts { get; set; }
        public DbSet<Domain.Entities.Comment> Comments { get; set; }
    }
}
