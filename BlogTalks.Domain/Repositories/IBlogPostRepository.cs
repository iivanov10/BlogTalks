using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        PagedResult<BlogPost> GetAll(int? pageNumber, int? pageSize, string? searchWord, string? tag);
    }
}
