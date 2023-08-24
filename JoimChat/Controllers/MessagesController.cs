using JoimChat.Models;
using JoimChat.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JoimChat.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMessage([FromBody] Message message)
        {
            if (message == null)
            {
                return BadRequest("Message is null");
            }

            await _messagesService.CreateMessage(message);

            return CreatedAtRoute(nameof(GetMessageById), new { id = message.MessageId}, message);
        }

        [HttpGet("get/messageId")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMessageById([FromQuery] int messageId)
        {
            if (messageId < 0)
            {
                return BadRequest();
            }

            return Ok(await _messagesService.GetMessageById(messageId));
        }

        [HttpGet("get/messageString")]
        [Produces("application/json")]
        public async Task<IActionResult> GetMessageByString([FromQuery] string messageString)
        {
            var response = await _messagesService.GetMessageByString(messageString);
            
            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpDelete("get/delete")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteMessage([FromQuery] int messageId)
        {
            await _messagesService.DeleteMessageById(messageId);

            return Ok();
        }

        [HttpGet("get/messages/sent")]
        [Produces("application/json")]
        public async Task<IActionResult> GetSentMessages([FromQuery] int senderId)
        {
            var response = await _messagesService.GetSentMessagesByUserId(senderId);

            if (response == null) return NotFound();

            return Ok(response);
        }

        [HttpGet("get/messages/received")]
        [Produces("application/json")]
        public async Task<IActionResult> GetReceivedMessages([FromQuery] int recipientId)
        {
            var response = await _messagesService.GetReceivedMessagesByUserId(recipientId);

            if (response == null) return NotFound();

            return Ok(response);
        }
    }
}
