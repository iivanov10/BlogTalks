using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public class DeleteByIdHandler : IRequestHandler<DeleteByIdRequest, DeleteByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;
        public DeleteByIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<DeleteByIdResponse> Handle(DeleteByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = _fakeDataStore.GetCommentById(request.Id);
            await _fakeDataStore.DeleteComment(comment.Result.Id);

            return new DeleteByIdResponse();
        }
    }
}
