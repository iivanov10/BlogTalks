using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public class UpdateByIdHandler : IRequestHandler<UpdateByIdRequest, UpdateByIdResponse>
    {
        private readonly FakeDataStore _fakeDataStore;
        public UpdateByIdHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<UpdateByIdResponse> Handle(UpdateByIdRequest request, CancellationToken cancellationToken)
        {
            var comment = _fakeDataStore.GetCommentById(request.Id);
            if (comment == null)
            {
                return null;
            }

            var commentDTO = new CommentDTO
            {
                Id = request.Id,
                Text = request.Text,
                BlogPostId = request.BlogPostId,
                CreatedBy = request.CreatedBy,
                CreatedAt = request.CreatedAt,
            };

            await _fakeDataStore.UpdateComment(comment.Result.Id, commentDTO);

            return new UpdateByIdResponse();
        }
    }
}
