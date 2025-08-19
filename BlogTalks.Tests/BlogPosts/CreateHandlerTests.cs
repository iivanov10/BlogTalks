using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;

namespace BlogTalks.Tests.BlogPosts
{
    public class CreateHandlerTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        private readonly Mock<IHttpContextAccessor> _contextAccessorMock;

        public CreateHandlerTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
            _contextAccessorMock = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public async Task Handle_Should_CreateBlogPost_WhenCalled()
        {
            var request = new CreateRequest("Title", "Text", 1, new List<string> { "Tag1" });

            var claimsIdentity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, request.CreatedBy.ToString())
            });

            _contextAccessorMock
                .Setup(c => c.HttpContext.User)
                .Returns(new ClaimsPrincipal(claimsIdentity));

            var handler = new CreateHandler(_blogPostRepositoryMock.Object, _contextAccessorMock.Object);

            await handler.Handle(request, default);

            _blogPostRepositoryMock.Verify(r => r.Add(It.Is<BlogPost>(bp =>
                bp.Title == request.Title &&
                bp.Text == request.Text &&
                bp.CreatedBy == request.CreatedBy &&
                bp.Tags == request.Tags)), Times.Once);
        }

        [Fact]
        public async Task Handle_Should_ThrowBlogTalksException_WhenRepositoryThrowsException()
        {
            var request = new CreateRequest("Title", "Text", 0, new List<string>());

            _blogPostRepositoryMock
                .Setup(r => r.Add(It.IsAny<BlogPost>()))
                .Throws(new BlogTalksException());

            var handler = new CreateHandler(_blogPostRepositoryMock.Object, _contextAccessorMock.Object);
            
            await Assert.ThrowsAsync<BlogTalksException>(() =>
                handler.Handle(request, default));
            
            _blogPostRepositoryMock.Verify(r => r.Add(It.IsAny<BlogPost>()), Times.Once);
        }
    }
}
