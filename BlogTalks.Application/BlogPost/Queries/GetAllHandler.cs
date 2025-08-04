using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
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
            var blogPosts = await _fakeDataStore.GetAllBlogPosts();

            return blogPosts.Select(bp => new GetAllResponse
            {
                Id = bp.Id,
                Title = bp.Title,
                Text = bp.Text,
                CreatedBy = bp.CreatedBy,
                Timestamp = bp.Timestamp,
                Tags = bp.Tags,
                Comments = bp.Comments
            });
        }
    }
}
