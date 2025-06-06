using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Controllers;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Service.Interfaces;
using Xunit;

namespace Vet4PetAPI.Tests
{
    public class UserControllerTests
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _userServiceMock = new Mock<IUserService>();
            _controller = new UserController(_userServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfUsers()
        {
            var users = new List<User> { new User { Id = 1, Name = "John" } };
            _userServiceMock.Setup(s => s.GetAllUsersAsync()).ReturnsAsync(users);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(users, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenUserExists()
        {
            var user = new User { Id = 1, Name = "John" };
            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync(user);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(user, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenUserDoesNotExist()
        {
            _userServiceMock.Setup(s => s.GetUserByIdAsync(1)).ReturnsAsync((User)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var user = new User { Id = 1, Name = "John" };
            _userServiceMock.Setup(s => s.CreateUserAsync(user, "password")).ReturnsAsync(user);

            var result = await _controller.Create(user, "password");

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(user, createdResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenIdsMatch()
        {
            var user = new User { Id = 1, Name = "John" };

            var result = await _controller.Update(1, user);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var user = new User { Id = 2, Name = "John" };

            var result = await _controller.Update(1, user);

            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent()
        {
            var result = await _controller.Delete(1);
            Assert.IsType<NoContentResult>(result);
        }
    }
} 