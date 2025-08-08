using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;

        public LoginHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                return await Task.FromResult(new LoginResponse { Token = "User not found." });
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                return await Task.FromResult(new LoginResponse { Token = "Invalid password." });
            }

            return await Task.FromResult(new LoginResponse { Token = "", RefreshToken = "" });
        }
    }
}
