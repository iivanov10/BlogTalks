using BlogTalks.Domain.Entities;

namespace BlogTalks.Domain.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        User? GetByName(string name);
        User? GetByEmail(string email);
    }
}
