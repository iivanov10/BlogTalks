using BlogTalks.Application.BlogPost.Queries;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace BlogTalks.Tests.BlogPosts
{
    public class GetByIdHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;

        public GetByIdHandlerTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        }

        [Fact]
        public async Task Handle_Should_ReturnBlogPost_WhenCalled()
        {
            var blogPost = new BlogPost
            {
                Id = 1,
                Title = "Title",
                Text = "Text",
                CreatedBy = 0,
                CreatedAt = DateTime.UtcNow,
                Tags = new List<string>() { "Tag1" },
                Comments = new List<Comment>()
            };
            _blogPostRepositoryMock
                .Setup(r => r.GetById(1))
                .Returns(blogPost);

            var handler = new GetByIdHandler(_blogPostRepositoryMock.Object);

            var result = await handler.Handle(new GetByIdRequest(1), default);

            var blogPostResult = new GetByIdResponse
            {
                Title = blogPost.Title,
                Text = blogPost.Text,
                CreatedBy = blogPost.CreatedBy,
                CreatedAt = blogPost.CreatedAt,
                Tags = blogPost.Tags,
            };

            result.Should().BeEquivalentTo(blogPostResult);
        }

        [Fact]
        public async Task Handle_Should_ThrowBlogTalksException_WhenBlogPostNotFound()
        {
            _blogPostRepositoryMock
                .Setup(r => r.GetById(1))
                .Throws(new BlogTalksException());

            var request = new GetByIdRequest(1);

            var handler = new GetByIdHandler(_blogPostRepositoryMock.Object);

            await Assert.ThrowsAsync<BlogTalksException>(() =>
            {
                return handler.Handle(request, default);
            });
        }
    }
}
