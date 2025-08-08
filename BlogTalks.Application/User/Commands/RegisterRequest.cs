using MediatR;

namespace BlogTalks.Application.User.Commands
{
    public record RegisterRequest(string Name, string Email, string Password) : IRequest<RegisterResponse>;
}
