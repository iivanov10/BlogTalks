using MediatR;
using System.Text.Json.Serialization;

namespace BlogTalks.Application.Comment.Commands
{
    public record CreateRequest(string Text, [property: JsonIgnore]int CreatedBy, int BlogPostId) : IRequest<CreateResponse>;
}