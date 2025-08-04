using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public record CreateRequest(int Id, string Text, DateTime CreatedAt, int CreatedBy, int BlogPostId) : IRequest<CreateResponse>;
}