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
    public class AppointmentControllerTests
    {
        private readonly Mock<IAppointmentService> _appointmentServiceMock;
        private readonly AppointmentController _controller;

        public AppointmentControllerTests()
        {
            _appointmentServiceMock = new Mock<IAppointmentService>();
            _controller = new AppointmentController(_appointmentServiceMock.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithListOfAppointments()
        {
            var appointments = new List<Appointment> { new Appointment { Id = 1, Description = "Checkup" } };
            _appointmentServiceMock.Setup(s => s.GetAllAppointmentsAsync()).ReturnsAsync(appointments);

            var result = await _controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(appointments, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsOkResult_WhenAppointmentExists()
        {
            var appointment = new Appointment { Id = 1, Description = "Checkup" };
            _appointmentServiceMock.Setup(s => s.GetAppointmentByIdAsync(1)).ReturnsAsync(appointment);

            var result = await _controller.GetById(1);

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(appointment, okResult.Value);
        }

        [Fact]
        public async Task GetById_ReturnsNotFound_WhenAppointmentDoesNotExist()
        {
            _appointmentServiceMock.Setup(s => s.GetAppointmentByIdAsync(1)).ReturnsAsync((Appointment)null);

            var result = await _controller.GetById(1);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtActionResult()
        {
            var appointment = new Appointment { Id = 1, Description = "Checkup" };
            _appointmentServiceMock.Setup(s => s.CreateAppointmentAsync(appointment)).ReturnsAsync(appointment);

            var result = await _controller.Create(appointment);

            var createdResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(appointment, createdResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsNoContent_WhenIdsMatch()
        {
            var appointment = new Appointment { Id = 1, Description = "Checkup" };

            var result = await _controller.Update(1, appointment);

            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Update_ReturnsBadRequest_WhenIdsDoNotMatch()
        {
            var appointment = new Appointment { Id = 2, Description = "Checkup" };

            var result = await _controller.Update(1, appointment);

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