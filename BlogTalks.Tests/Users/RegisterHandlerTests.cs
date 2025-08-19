using BlogTalks.Domain.Repositories;
using Moq;
using FluentAssertions;
using BlogTalks.Application.User.Commands;
using BlogTalks.Domain.Entities;

namespace BlogTalks.Tests.Users
{
    public class RegisterHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public RegisterHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureMessage_WhenEmailIsNotUnique()
        {
            var request = new RegisterRequest("Name", "name@email.com", "password");

            _userRepositoryMock
                .Setup(r => r.GetByEmail(request.Email))
                .Returns(new User { Email = request.Email });

            var handler = new RegisterHandler(_userRepositoryMock.Object);

            var result = await handler.Handle(request, default);

            result.Message.Should().Be($"Email already exists.");
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessMessage_WhenEmailIsUnique()
        {
            var request = new RegisterRequest("Name", "uniqueemail@mail.com", "password");

            _userRepositoryMock
                .Setup(r => r.GetByEmail(request.Email))
                .Returns(null as User);

            var handler = new RegisterHandler(_userRepositoryMock.Object);

            var result = await handler.Handle(request, default);

            result.Message.Should().Be("User registered successfully.");
        }
    }
}
