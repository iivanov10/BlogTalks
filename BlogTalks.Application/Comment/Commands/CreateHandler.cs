using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IBlogPostRepository _blogPostRepository;

        public CreateHandler(ICommentRepository commentRepository, IBlogPostRepository blogPostRepository)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
        }

        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
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
