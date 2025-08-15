using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public record class GetAllRequest(int? PageNumber, int? PageSize, string? SearchWord, string? Tag) : IRequest<GetAllResponse>;
}
