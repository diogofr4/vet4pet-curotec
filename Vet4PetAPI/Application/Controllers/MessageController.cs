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
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Message>>> GetAll()
        {
            var messages = await _messageService.GetAllMessagesAsync();
            return Ok(messages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> GetById(int id)
        {
            var message = await _messageService.GetMessageByIdAsync(id);
            if (message == null)
                return NotFound();
            return Ok(message);
        }

        [HttpGet("by-animal/{animalId}")]
        public async Task<ActionResult<IEnumerable<Message>>> GetByAnimal(int animalId)
        {
            var messages = await _messageService.GetMessagesByAnimalAsync(animalId);
            return Ok(messages);
        }

        [HttpGet("between-users")]
        public async Task<ActionResult<IEnumerable<Message>>> GetBetweenUsers(
            [FromQuery] int senderId,
            [FromQuery] int receiverId,
            [FromQuery] int animalId)
        {
            var messages = await _messageService.GetMessagesBetweenUsersAsync(senderId, receiverId, animalId);
            return Ok(messages);
        }

        [HttpPost]
        public async Task<ActionResult<Message>> SendMessage([FromBody] Message message)
        {
            var sentMessage = await _messageService.SendMessageAsync(message);
            return Ok(sentMessage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMessage(int id, [FromBody] Message message)
        {
            if (id != message.Id)
                return BadRequest();

            await _messageService.UpdateMessageAsync(message);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _messageService.DeleteMessageAsync(id);
            return NoContent();
        }
    }
} 