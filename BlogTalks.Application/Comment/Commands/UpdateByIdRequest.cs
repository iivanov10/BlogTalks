using MediatR;
using System.Text.Json.Serialization;

namespace BlogTalks.Application.Comment.Commands
{
    public record UpdateByIdRequest(
        [property: JsonIgnore] int Id,
        string Text) : IRequest<UpdateByIdResponse>;
}
