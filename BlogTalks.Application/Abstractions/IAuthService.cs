namespace BlogTalks.Application.Abstractions
{
    public interface IAuthService
    {
        public string CreateToken(int userId, string name, string email, IList<string> roles);
    }
}
