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
    public class AnimalControllerTests
    {
        private readonly Mock<IAnimalService> _animalServiceMock;
        private readonly AnimalController _controller;

        public AnimalControllerTests()
        {
            _animalServiceMock = new Mock<IAnimalService>();
            _controller = new AnimalController(_animalServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAnimals()
        {
            // Arrange
            var animals = new List<Animal> { new Animal { Id = 1, Name = "Dog" } };
            _animalServiceMock.Setup(s => s.GetAllAnimalsAsync()).ReturnsAsync(animals);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(animals, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenAnimalExists()
        {
            var animal = new Animal { Id = 1, Name = "Dog" };
            _animalServiceMock.Setup(s => s.GetAnimalByIdAsync(1)).ReturnsAsync(animal);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(animal, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAnimalDoesNotExist()
        {
            _animalServiceMock.Setup(s => s.GetAnimalByIdAsync(1)).ReturnsAsync((Animal)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var animal = new Animal { Id = 1, Name = "Dog" };
            _animalServiceMock.Setup(s => s.CreateAnimalAsync(animal)).ReturnsAsync(animal);

            var result = await _controller.Create(animal);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(animal, createdResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenIdsMatch()
        {
            var animal = new Animal { Id = 1, Name = "Dog" };

            var result = await _controller.Update(1, animal);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var animal = new Animal { Id = 2, Name = "Dog" };

            var result = await _controller.Update(1, animal);

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