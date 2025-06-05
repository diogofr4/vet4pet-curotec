using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Service.Interfaces;
using Domain;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("by-animal")]
        public async Task<ActionResult<PaginatedResponse<Appointment>>> GetByAnimal(
            [FromQuery] int animalId,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var appointments = await _appointmentService.GetAppointmentsByAnimalAsync(animalId, page, pageSize);
            return Ok(appointments);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAll()
        {
            var appointments = await _appointmentService.GetAllAppointmentsAsync();
            return Ok(appointments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Appointment>> GetById(int id)
        {
            var appointment = await _appointmentService.GetAppointmentByIdAsync(id);
            if (appointment == null) return NotFound();
            return Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] Appointment appointment)
        {
            var createdAppointment = await _appointmentService.CreateAppointmentAsync(appointment);
            return CreatedAtAction(nameof(GetById), new { id = createdAppointment.Id }, createdAppointment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Appointment appointment)
        {
            if (id != appointment.Id) return BadRequest();
            await _appointmentService.UpdateAppointmentAsync(appointment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _appointmentService.DeleteAppointmentAsync(id);
            return NoContent();
        }
    }

    public class PaginatedResponse<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
    }
} 