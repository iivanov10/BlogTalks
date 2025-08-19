using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace BlogTalks.Tests.BlogPosts
{
    public class DeleteByIdTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;

        public DeleteByIdTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        }

        [Fact]
        public async Task Handle_Should_DeleteBlogPost_WhenCalled()
        {
            var blogPost = new BlogPost()
            {
                Id = 1,
            };

            _blogPostRepositoryMock
                .Setup(r => r.GetById(blogPost.Id))
                .Returns(blogPost);

            var request = new DeleteByIdRequest(blogPost.Id);

            var handler = new DeleteByIdHandler(_blogPostRepositoryMock.Object);

            var result = await handler.Handle(request, default);

            _blogPostRepositoryMock.Verify(r => r.Delete(blogPost), Times.Once);
            Assert.DoesNotContain(blogPost, _blogPostRepositoryMock.Object.GetAll());
        }

        [Fact]
        public async Task Handle_Should_ThrowBlogTalksException_WhenBlogPostNotFound()
        {
            _blogPostRepositoryMock
                .Setup(r => r.GetById(1))
                .Throws(new BlogTalksException());

            var request = new DeleteByIdRequest(1);

            var handler = new DeleteByIdHandler(_blogPostRepositoryMock.Object);

            await Assert.ThrowsAsync<BlogTalksException>(() =>
            {
                return handler.Handle(request, default);
            });
        }
    }
}
