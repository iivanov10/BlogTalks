using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public GetByIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }
        
        public async Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = await _fakeDataStore.GetBlogPostById(request.Id);
            if (blogPost == null)
            {
                return null;
            }
            return new GetByIdResponse
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.Timestamp,
                Tags = blogPost.Tags,
                Comments = blogPost.Comments
            };
        }
    }
}
