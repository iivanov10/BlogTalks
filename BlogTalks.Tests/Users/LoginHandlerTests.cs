using BlogTalks.Application.Abstractions;
using BlogTalks.Application.User.Commands;
using BlogTalks.Domain.Entities;
using BlogTalks.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace BlogTalks.Tests.Users
{
    public class LoginHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IAuthService> _authServiceMock;

        public LoginHandlerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authServiceMock = new Mock<IAuthService>();
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureMessage_WhenUserNotFound()
        {
            var request = new LoginRequest("email@email.com", "password");

            _userRepositoryMock
                .Setup(r => r.GetByEmail(request.Email))
                .Returns(null as User);

            var handler = new LoginHandler(_userRepositoryMock.Object, _authServiceMock.Object);
            
            var result = await handler.Handle(request, default);

            result.Message.Should().Be("User not found.");
        }

        [Fact]
        public async Task Handle_Should_ReturnFailureMessage_WhenPasswordIsInvalid()
        {
            var request = new LoginRequest("email@email.com", "password");

            var user = new User { Email = request.Email, Password = PasswordHasher.HashPassword("randomPassword") };
            _userRepositoryMock
                .Setup(r => r.GetByEmail(request.Email))
                .Returns(user);

            var handler = new LoginHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            var result = await handler.Handle(request, default);

            result.Message.Should().Be("Invalid password.");
        }

        [Fact]
        public async Task Handle_Should_ReturnSuccessMessage_WhenLoginIsSuccessful()
        {
            var request = new LoginRequest("Name@email.com", "password");

            var user = new User
            {
                Id = 0,
                Name = "Name",
                Email = request.Email,
                Password = PasswordHasher.HashPassword(request.Password)
            };
            _userRepositoryMock
                .Setup(r => r.GetByEmail(request.Email))
                .Returns(user);

            _authServiceMock
                .Setup(a => a.CreateToken(user.Id, user.Name, user.Email, new List<string>()))
                .Returns("token");

            var handler = new LoginHandler(_userRepositoryMock.Object, _authServiceMock.Object);

            var result = await handler.Handle(request, default);
            
            result.Token.Should().Be("token");
        }
    }
}
