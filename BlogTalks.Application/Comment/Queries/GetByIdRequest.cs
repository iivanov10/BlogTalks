using MediatR;

namespace BlogTalks.Application.Comment.Queries
{
    public record GetByIdRequest(int Id) : IRequest<GetByIdResponse>;
}