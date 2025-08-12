using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateHandler(IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            int userIdValue = 0;
            if (int.TryParse(userId, out int parsedUserId))
            {
                userIdValue = parsedUserId;
            }

            var blogPost = new Domain.Entities.BlogPost
            {
                Title = request.Title,
                Text = request.Text,
                CreatedBy = userIdValue,
                Tags = request.Tags
            };

            _blogPostRepository.Add(blogPost);

            return Task.FromResult(new CreateResponse { Id = blogPost.Id });
        }
    }
}
