using BlogTalks.Application.Contracts;
using BlogTalks.Domain.Repositories;
using MediatR;

namespace BlogTalks.Application.BlogPost.Queries
{
    public class GetAllHandler : IRequestHandler<GetAllRequest, GetAllResponse>
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IUserRepository _userRepository;

        public GetAllHandler(IBlogPostRepository blogPostRepository, IUserRepository userRepository)
        {
            _blogPostRepository = blogPostRepository;
            _userRepository = userRepository;
        }

        public Task<GetAllResponse> Handle(GetAllRequest request, CancellationToken cancellationToken)
        {
            var pagedResult = _blogPostRepository.GetAll(
                request.PageNumber ?? 1,
                request.PageSize ?? 10,
                request.SearchWord,
                request.Tag
            );

            var blogPosts = pagedResult?.Items;
            
            var blogPostsModel = blogPosts?.Select(bp => new BlogPostModel
            {
                Id = bp.Id,
                Title = bp.Title,
                Text = bp.Text,
                Tags = bp.Tags
            }).ToList();

            var userIds = blogPosts?.Select(bp => bp.CreatedBy).Distinct().ToList();
            var users = _userRepository.GetByIds(userIds);

            if (users != null)
            {
                for (int i = 0; i < blogPosts.Count; i++)
                {
                    string creatorName = users.FirstOrDefault(u => u.Id == blogPosts[i].CreatedBy)?.Name ?? string.Empty;
                    blogPostsModel[i].CreatorName = creatorName;
                }
            }

            var totalCount = pagedResult?.TotalCount ?? 0;

            int totalPages = 1;
            if (request.PageSize != null && request.PageSize > 0)
            {
                totalPages = (totalCount / request.PageSize.Value) + (totalCount % request.PageSize.Value > 0 ? 1 : 0);
            }
            
            var metadata = new Metadata
            {
                TotalCount = totalCount,
                PageSize = request.PageSize ?? totalCount,
                PageNumber = request.PageNumber ?? 1,
                TotalPages = totalPages
            };

            return Task.FromResult(new GetAllResponse
            {
                BlogPosts = blogPostsModel,
                Metadata = metadata
            });
        }
    }
}
