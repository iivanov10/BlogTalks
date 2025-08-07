using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public GetAllHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var blogPosts = _blogPostRepository.GetAll();

            return Task.FromResult(blogPosts.Select(bp => new GetAllResponse
            {
                Id = bp.Id,
                Title = bp.Title,
                Text = bp.Text,
                CreatedBy = bp.CreatedBy,
                Timestamp = bp.CreatedAt,
                Tags = bp.Tags,
            }));
        }
    }
}
