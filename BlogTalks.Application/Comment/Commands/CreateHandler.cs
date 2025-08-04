using BlogTalks.Domain.DTOs;
using MediatR;

namespace BlogTalks.Application.Comment.Commands
{
    public class CreateHandler : IRequestHandler<CreateRequest, CreateResponse>
    {
        private readonly FakeDataStore _fakeDataStore;

        public CreateHandler(FakeDataStore fakeDataStore)
        {
            _fakeDataStore = fakeDataStore;
        }

        public async Task<CreateResponse> Handle(CreateRequest request, CancellationToken cancellationToken)
        {
            var commentDTO = new CommentDTO
            {
                Id = request.Id,
                Text = request.Text,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                BlogPostId = request.BlogPostId
            };

            await _fakeDataStore.CreateComment(commentDTO);

            return new CreateResponse
            {
                Id = request.Id,
                Text = request.Text,
                CreatedAt = request.CreatedAt,
                CreatedBy = request.CreatedBy,
                BlogPostId = request.BlogPostId
            };
        }
    }
}
