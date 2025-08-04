using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public record class GetAllRequest : IRequest<IEnumerable<GetAllResponse>>;
}
