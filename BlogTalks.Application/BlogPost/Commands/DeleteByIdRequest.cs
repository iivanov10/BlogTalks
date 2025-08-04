using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public record DeleteByIdRequest(int Id) : IRequest<DeleteByIdResponse>;
}
