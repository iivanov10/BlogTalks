using MediatR;
using System.Text.Json.Serialization;

namespace BlogTalks.Application.BlogPost.Commands
{
    public record CreateRequest(
        string Title,
        string Text,
        [property: JsonIgnore]int CreatedBy,
        List<string> Tags) : IRequest<CreateResponse>;
}
