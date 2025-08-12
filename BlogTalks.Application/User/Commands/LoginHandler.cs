using BlogTalks.Application.Abstractions;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Commands
{
    public class LoginHandler : IRequestHandler<LoginRequest, LoginResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;

        public LoginHandler(IUserRepository userRepository, IAuthService authService)
        {
            _userRepository = userRepository;
            _authService = authService;
        }

        public async Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var user = _userRepository.GetByEmail(request.Email);
            if (user == null)
            {
                return await Task.FromResult(new LoginResponse { Message = "User not found." });
            }

            if (!PasswordHasher.VerifyPassword(request.Password, user.Password))
            {
                return await Task.FromResult(new LoginResponse { Message = "Invalid password." });
            }

            var token = _authService.CreateToken(user.Id, user.Name, user.Email, new List<string>());

            return await Task.FromResult(new LoginResponse { Token = token, RefreshToken = "" });
        }
    }
}
