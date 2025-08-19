using BlogTalks.Application.BlogPost.Queries;
using BlogTalks.Application.Contracts;
using BlogTalks.Domain.DTOs;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace BlogTalks.Tests.BlogPosts
{
    public class GetAllHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public GetAllHandlerTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handle_Should_ReturnAllBlogPosts_WhenCalled()
        {
            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = 1,
                    Title = "Title1",
                    Text = "Text1",
                    CreatedBy = 0,
                    CreatedAt = DateTime.UtcNow,
                    Tags = new List<string>() { "Tag1" },
                    Comments = new List<Comment>()
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Title2",
                    Text = "Text2",
                    CreatedBy = 1,
                    CreatedAt = DateTime.UtcNow,
                    Tags = new List<string>(),
                    Comments = new List<Comment>()
                }
            };
            _blogPostRepositoryMock
                .Setup(r => r.GetAll(1, 10, null, null))
                .Returns(new PagedResult<BlogPost>(blogPosts, blogPosts.Count));

            _userRepositoryMock
                .Setup(r => r.GetByIds(new List<int>()))
                .Returns(new List<User>
                {
                    new User { Id = 0, Name = "User1" },
                    new User { Id = 1, Name = "User2" }
                });

            var handler = new GetAllHandler(_blogPostRepositoryMock.Object, _userRepositoryMock.Object);

            var result = await handler.Handle(new GetAllRequest(1, 10, null, null), default);

            var blogPostsModel = blogPosts.Select(bp => new BlogPostModel
            {
                Id = bp.Id,
                Title = bp.Title,
                Text = bp.Text,
                Tags = bp.Tags,
                CreatorName = _userRepositoryMock.Object.GetById(bp.CreatedBy)?.Name ?? string.Empty
            }).ToList();

            var metadata = new Metadata
            {
                TotalCount = blogPosts.Count,
                PageSize = 10,
                PageNumber = 1,
                TotalPages = 1
            };

            result.BlogPosts.Should().BeEquivalentTo(blogPostsModel);
            result.Metadata.Should().BeEquivalentTo(metadata);
        }
        
        [Fact]
        public async Task Handle_ShouldReturnEmptyList_WhenNoBlogPostsExist()
        {
            _blogPostRepositoryMock
                .Setup(r => r.GetAll(1, 10, null, null))
                .Returns(new PagedResult<BlogPost>(new List<BlogPost>(), 0));

            var handler = new GetAllHandler(_blogPostRepositoryMock.Object, _userRepositoryMock.Object);
            
            var result = await handler.Handle(new GetAllRequest(1, 10, null, null), default);
            
            result.Should().NotBeNull();
            result.BlogPosts.Should().BeEmpty();
        }
    }
}
