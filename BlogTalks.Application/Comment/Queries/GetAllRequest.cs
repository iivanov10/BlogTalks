using MediatR;

namespace BlogTalks.Application.Comment.Queries
{
    public record GetAllRequest() : IRequest<IEnumerable<GetAllResponse>>;
}
