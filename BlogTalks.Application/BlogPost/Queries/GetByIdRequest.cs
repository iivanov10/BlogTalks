using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public record GetByIdRequest(int Id) : IRequest<GetByIdResponse>;
}
