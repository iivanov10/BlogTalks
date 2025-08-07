using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetByIdHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.Id);
            if (blogPost == null)
            {
                return null;
            }

            return Task.FromResult(new GetByIdResponse
            {
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                Timestamp = blogPost.CreatedAt,
                Tags = blogPost.Tags,
            });
        }
    }
}
