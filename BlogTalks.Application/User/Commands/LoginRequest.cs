using MediatR;

namespace BlogTalks.Application.User.Commands
{
    public record LoginRequest(string Email, string Password) : IRequest<LoginResponse>;
}
