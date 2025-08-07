using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public class DeleteByIdHandler : IRequestHandler<DeleteByIdRequest, DeleteByIdResponse>
    {
        private readonly ICommentRepository _commentRepository;

        public DeleteByIdHandler(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public Task<DeleteByIdResponse> Handle(DeleteByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = _commentRepository.GetById(request.Id);
            _commentRepository.Delete(comment);

            return Task.FromResult(new DeleteByIdResponse());
        }
    }
}
