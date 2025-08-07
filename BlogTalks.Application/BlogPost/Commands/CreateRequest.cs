using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public record CreateRequest(
        string Title,
        string Text,
        int CreatedBy,
        List<string> Tags) : IRequest<CreateResponse>;
}
