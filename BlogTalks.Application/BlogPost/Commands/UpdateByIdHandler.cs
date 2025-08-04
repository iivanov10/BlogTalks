using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class UpdateByIdHandler : IRequestHandler<UpdateByIdRequest, UpdateByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public UpdateByIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<UpdateByIdResponse> Handle(UpdateByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _fakeDataStore.GetBlogPostById(request.Id);
            if (blogPost == null)
            {
                return null;
            }
            
            var blogPostDTO = new BlogPostDTO
            {
                Id = blogPost.Result.Id,
                Title = blogPost.Result.Title,
                Text = blogPost.Result.Text,
                CreatedBy = blogPost.Result.CreatedBy,
                Timestamp = blogPost.Result.Timestamp,
                Tags = blogPost.Result.Tags,
                Comments = blogPost.Result.Comments
            };

            await _fakeDataStore.UpdateBlogPost(blogPost.Result.Id, blogPostDTO);

            return new UpdateByIdResponse();
        }
    }
}
