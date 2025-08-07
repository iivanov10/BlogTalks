using MediatR;
using BlogTalks.Domain.Repositories;

namespace BlogTalks.Application.Comment.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, IEnumerable<GetAllResponse>>
    {
        private readonly ICommentRepository _commentRepository;

        public GetAllHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<IEnumerable<GetAllResponse>> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var comments = _commentRepository.GetAll();

            var response = comments.Select(c => new GetAllResponse
            {
                Id = c.Id,
                Text = c.Text,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                BlogPostId = c.BlogPostId,
            });

            return Task.FromResult(response);
        }
    }
}
