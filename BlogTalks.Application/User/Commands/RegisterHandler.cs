using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.User.Commands
{
    public class RegisterHandler : IRequestHandler<RegisterRequest, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;

        public RegisterHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<RegisterResponse> Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var existingUser = _userRepository.GetByName(request.Name);
            if (existingUser != null)
            {
                return Task.FromResult(new RegisterResponse { Message = "Username already exists." });
            }

            existingUser = _userRepository.GetByEmail(request.Email);
            if (existingUser != null)
            {
                return Task.FromResult(new RegisterResponse { Message = "Email already exists." });
            }

            var user = new Domain.Entities.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = PasswordHasher.HashPassword(request.Password)
            };

            _userRepository.Add(user);

            return Task.FromResult(new RegisterResponse { Message = "User registered successfully." });
        }
    }
}
