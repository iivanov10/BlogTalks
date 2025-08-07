using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public CreateHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var blogPost = new Domain.Entities.BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedBy = request.CreatedBy,
                Tags = request.Tags
            };

            _blogPostRepository.Add(blogPost);

            return Task.FromResult(new CreateResponse { Id = blogPost.Id });
        }
    }
}
