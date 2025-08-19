using BlogTalks.Application.BlogPost.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Exceptions;
using BlogTalks.Domain.Repositories;
using Moq;

namespace BlogTalks.Tests.BlogPosts
{
    public class UpdateByIdTests
    {
        private readonly Mock<IBlogPostRepository> _blogPostRepositoryMock;
        
        public UpdateByIdTests()
        {
            _blogPostRepositoryMock = new Mock<IBlogPostRepository>();
        }

        [Fact]
        public async Task Handle_Should_UpdateBlogPost_WhenCalled()
        {
            var blogPost = new BlogPost()
            {
                Id = 1,
                Title = "Title",
                Text = "Text",
                Tags = new List<string>() { "Tag1" },
                Comments = new List<Comment>(),
                CreatedAt = DateTime.Now,
                CreatedBy = 1
            };

            _blogPostRepositoryMock
                .Setup(r => r.GetById(blogPost.Id))
                .Returns(blogPost);

            var request = new UpdateByIdRequest(blogPost.Id, "Updated title", "Updated text", new List<string>() { "Updated tags" });
            
            var handler = new UpdateByIdHandler(_blogPostRepositoryMock.Object);
            
            var result = await handler.Handle(request, default);
            
            Assert.Equal("Updated title", blogPost.Title);
            Assert.Equal("Updated text", blogPost.Text);
            Assert.Equal(new List<string>() { "Updated tags" }, blogPost.Tags);
        }

        [Fact]
        public async Task Handle_Should_ThrowBlogTalksException_WhenBlogPostNotFound()
        {
            _blogPostRepositoryMock
                .Setup(r => r.GetById(1))
                .Throws(new BlogTalksException());
            
            var request = new UpdateByIdRequest(1, "Title", "Text", new List<string>());
            
            var handler = new UpdateByIdHandler(_blogPostRepositoryMock.Object);
            
            await Assert.ThrowsAsync<BlogTalksException>(() =>
            {
                return handler.Handle(request, default);
            });
        }
    }
}
