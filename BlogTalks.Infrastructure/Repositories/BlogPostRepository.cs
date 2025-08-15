using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using BlogTalks.Infrastructure.Data.DataContext;

namespace BlogTalks.Infrastructure.Repositories
{
    public class BlogPostRepository : GenericRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(ApplicationDbContext context) : base(context, context.BlogPosts)
        {

        }

        public PagedResult<BlogPost> GetAll(int? pageNumber, int? pageSize, string? searchWord, string? tag)
        {
            var query = _dbSet.AsQueryable();

            if (!string.IsNullOrEmpty(searchWord))
            {
                query = query.Where(bp => bp.Title.Contains(searchWord) || bp.Text.Contains(searchWord));
            }

            if (!string.IsNullOrEmpty(tag))
            {
                query = query.Where(bp => bp.Tags.Any(t => t.Equals(tag)));
            }
            
            var totalCount = query.Count();

            if (pageNumber.HasValue && pageSize.HasValue)
            {
                query = query.Skip((pageNumber.Value - 1) * pageSize.Value).Take(pageSize.Value);
            }

            return new PagedResult<BlogPost>(query.ToList(), totalCount);
        }
    }
}
