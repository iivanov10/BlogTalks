using BlogTalks.Domain.Repositories;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Security.Claims;
using BlogTalks.Application.Abstractions;
using BlogTalks.Application.Contracts;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IUserRepository _userRepository;
        private readonly IFeatureManager _featureManager;
        private readonly IServiceProvider _serviceProvider;

        public CreateHandler(
            ICommentRepository commentRepository,
            IBlogPostRepository blogPostRepository,
            IHttpContextAccessor httpContextAccessor,
            IHttpClientFactory httpClientFactory,
            IUserRepository userRepository,
            IFeatureManager featureManager,
            IServiceProvider serviceProvider)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
            _userRepository = userRepository;
            _featureManager = featureManager;
            _serviceProvider = serviceProvider;
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

            var comment = new Domain.Entities.Comment
            {
                Text = request.Text,
                CreatedBy = request.CreatedBy,
                BlogPostId = request.BlogPostId,
                BlogPost = blogPost!
            };

            _commentRepository.Add(comment);

            var blogPostCreator = _userRepository.GetById(blogPost!.CreatedBy);
            var commentCreator = _userRepository.GetById(userIdValue);
            
            await SendEmail(commentCreator, blogPostCreator, blogPost);

            return new CreateResponse { Id = comment.Id };
        }

        private async Task SendEmail(Domain.Entities.User? commentCreator, Domain.Entities.User? blogPostCreator, Domain.Entities.BlogPost blogPost)
        {
            var dto = new EmailDTO()
            {
                From = commentCreator!.Email,
                To = blogPostCreator!.Email,
                Subject = "New comment added",
                Body = $"A new comment has been added to the blog post '{blogPost.Title}' by user {commentCreator.Name}."
            };

            if (await _featureManager.IsEnabledAsync("EmailHttpSender"))
            {
                var service = _serviceProvider.GetRequiredKeyedService<IMessagingService>("EmailHttpService");
                await service.Send(dto);

            }
            else if (await _featureManager.IsEnabledAsync("EmailRabbitMQSender"))
            {
                var service = _serviceProvider.GetRequiredKeyedService<IMessagingService>("EmailRabbitMQService");
                await service.Send(dto);
            }
            else
            {
                // todo add logger
            }
        }
    }
}
