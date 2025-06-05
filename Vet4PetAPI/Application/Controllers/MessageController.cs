using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Service.Interfaces;
using Domain;
using System.Collections.Generic;

namespace Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("appointment/{appointmentId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetByAppointmentId(int appointmentId)
        {
            var messages = await _messageService.GetMessagesByAppointmentIdAsync(appointmentId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> SendMessage([FromBody] Message message)
        {
            var sentMessage = await _messageService.SendMessageAsync(message);
            return Ok(sentMessage);
        }
    }
} 