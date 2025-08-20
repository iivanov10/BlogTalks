using BlogTalks.Domain.Repositories;
using BlogTalks.EmailSenderAPI.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using System.Security.Claims;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserRepository _userRepository;

        public CreateHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory, IUserRepository userRepository)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
        }

        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
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

            var httpClient = _httpClientFactory.CreateClient("EmailSenderAPI");

            var blogPostCreator = _userRepository.GetById(blogPost.CreatedBy);
            var commentCreator = _userRepository.GetById(userIdValue);
            EmailDTO dto = new EmailDTO()
            {
                From = commentCreator.Email,
                To = blogPostCreator.Email,
                Subject = "New comment added",
                Body = $"A new comment has been added to the blog post '{blogPost.Title}' by user {commentCreator.Name}."
            };

            await httpClient.PostAsJsonAsync("/email", dto, cancellationToken);

            return new CreateResponse { Id = comment.Id };
        }
    }
}
