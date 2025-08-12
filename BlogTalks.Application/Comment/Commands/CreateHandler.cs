using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst("userId")?.Value;
            int userIdValue = 0;
            if (int.TryParse(userId, out int parsedUserId))
            {
                userIdValue = parsedUserId;
            }

            var blogPost = _blogPostRepository.GetById(request.BlogPostId);
            if (blogPost == null)
            {
                return null;
            }

            var comment = new Domain.Entities.Comment
            {
                Text = request.Text,
                CreatedBy = request.CreatedBy,
                BlogPostId = request.BlogPostId,
                BlogPost = blogPost
            };

            _commentRepository.Add(comment);

            return new CreateResponse { Id = comment.Id };
        }
    }
}
