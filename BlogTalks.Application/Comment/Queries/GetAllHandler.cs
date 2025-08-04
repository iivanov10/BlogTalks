using MediatR;
using BlogTalks.Domain.DTOs;

namespace BlogTalks.Application.Comment.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetAllHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var comments = await _fakeDataStore.GetAllComments();

            var response = comments.Select(c => new GetAllResponse
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                BlogPostId = c.BlogPostId,
            });

            return response;
        }
    }
}
