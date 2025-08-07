using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class UpdateByIdHandler : IRequestHandler<UpdateByIdRequest, UpdateByIdResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public UpdateByIdHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<UpdateByIdResponse> Handle(UpdateByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                return null;
            }

            blogPost.Title = request.Title;
            blogPost.Text = request.Text;
            blogPost.Tags = request.Tags;

            _blogPostRepository.Update(blogPost);

            return new UpdateByIdResponse();
        }
    }
}
