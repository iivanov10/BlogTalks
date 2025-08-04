using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public record CreateRequest(
        int Id,
        string Title,
        string Text,
        int CreatedBy,
        DateTime Timestamp,
        List<string> Tags,
        List<CommentDTO> Comments) : IRequest<CreateResponse>;
}
