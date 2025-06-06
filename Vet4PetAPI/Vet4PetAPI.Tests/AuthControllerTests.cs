using System.Threading.Tasks;
using Application.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Interfaces;
using Xunit;

namespace Vet4PetAPI.Tests
{
    public class AuthControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly AuthController _controller;

        public AuthControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _controller = new AuthController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ReturnsOkResult_WithToken_WhenCredentialsAreValid()
        {
            var loginModel = new LoginModel { Email = "test@test.com", Password = "password" };
            var token = "valid.jwt.token";
            _authServiceMock.Setup(s => s.LoginAsync(loginModel.Email, loginModel.Password)).ReturnsAsync(token);

            var result = await _controller.Login(loginModel);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(token, okResult.Value);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenCredentialsAreInvalid()
        {
            var loginModel = new LoginModel { Email = "test@test.com", Password = "wrongpassword" };
            _authServiceMock.Setup(s => s.LoginAsync(loginModel.Email, loginModel.Password)).ReturnsAsync((string)null);

            var result = await _controller.Login(loginModel);

            Assert.IsType<UnauthorizedResult>(result.Result);
        }

        [Fact]
        public async Task Register_ReturnsCreatedAtActionResult_WhenRegistrationIsSuccessful()
        {
            var registerModel = new RegisterModel { Email = "test@test.com", Password = "password", Name = "Test User" };
            var user = new User { Id = 1, Email = registerModel.Email, Name = registerModel.Name };
            _authServiceMock.Setup(s => s.RegisterAsync(registerModel.Email, registerModel.Password, registerModel.Name)).ReturnsAsync(user);

            var result = await _controller.Register(registerModel);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(user, createdResult.Value);
        }

        [Fact]
        public async Task Register_ReturnsBadRequest_WhenEmailAlreadyExists()
        {
            var registerModel = new RegisterModel { Email = "existing@test.com", Password = "password", Name = "Test User" };
            _authServiceMock.Setup(s => s.RegisterAsync(registerModel.Email, registerModel.Password, registerModel.Name)).ReturnsAsync((User)null);

            var result = await _controller.Register(registerModel);

            Assert.IsType<BadRequestResult>(result.Result);
        }
    }
} 