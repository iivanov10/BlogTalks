using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public record UpdateByIdRequest(int Id, string Text, DateTime CreatedAt, int CreatedBy, int BlogPostId) : IRequest<UpdateByIdResponse>;
}
