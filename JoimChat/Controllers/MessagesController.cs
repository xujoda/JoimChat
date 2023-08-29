using JoimChat.Models;
using JoimChat.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JoimChat.Controllers
{
    [Route("api/messages")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesService _messagesService;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessagesController(IMessagesService messagesService, IHubContext<ChatHub> hubContext)
        {
            _messagesService = messagesService;
            _hubContext = hubContext;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateMessage([FromBody] MessageCreateModel messageModel)
        {
            if (messageModel == null)
            {
                return BadRequest("Message is null");
            }

            await _messagesService.CreateMessage(messageModel);
            await _hubContext.Clients.All.SendAsync("ReceiveMessage", "admin2", messageModel.MessageString);

            return Ok();
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
