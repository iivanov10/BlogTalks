using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public record DeleteByIdRequest(int Id) : IRequest<DeleteByIdResponse>;
}
