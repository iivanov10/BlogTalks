using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories
{
    public interface ICommentRepository : IRepository<Comment>
    {
        IEnumerable<Comment> GetCommentsByBlogPostId(int blogPostId);
    }
}
