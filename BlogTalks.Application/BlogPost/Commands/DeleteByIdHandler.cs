using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Commands
{
    public class DeleteByIdHandler : IRequestHandler<DeleteByIdRequest, DeleteByIdResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public DeleteByIdHandler(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        public Task<DeleteByIdResponse> Handle(DeleteByIdRequest request, CancellationToken cancellationToken)
        {
            var blogPost = _blogPostRepository.GetById(request.Id);
            _blogPostRepository.Delete(blogPost);

            return Task.FromResult(new DeleteByIdResponse());
        }
    }
}
