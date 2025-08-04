using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.Comment.Queries
{
    public class GetByBlogPostIdHandler : IRequestHandler<GetByBlogPostIdRequest, IEnumerable<GetByBlogPostIdResponse>>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetByBlogPostIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<IEnumerable<GetByBlogPostIdResponse>> Handle(GetByBlogPostIdRequest request, CancellationToken cancellationToken)
        {
            var comments = await _fakeDataStore.GetByBlogPostId(request.BlogPostId);
            return comments.Select(c => new GetByBlogPostIdResponse
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                BlogPostId = c.BlogPostId
            });
        }
    }
}
