using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Queries
{
    public class GetByIdHandler : IRequestHandler<GetByIdRequest, GetByIdResponse>
    {
        private readonly ICommentRepository _commentRepository;

        public GetByIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<GetByIdResponse> Handle(GetByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.Id);

            return Task.FromResult(new GetByIdResponse
            {
                Id = comment.Id,
                Text = comment.Text,
                CreatedAt = comment.CreatedAt,
                CreatedBy = comment.CreatedBy,
                BlogPostId = comment.BlogPostId,
            });
        }
    }
}
