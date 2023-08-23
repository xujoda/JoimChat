using JoimChat.Models;
using Microsoft.AspNetCore.Mvc;

namespace JoimChat.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        

        public UsersController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok();
        }
    }
}
