using JoimChat.Models;
using JoimChat.Services;
using Microsoft.AspNetCore.Mvc;

namespace JoimChat.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpPost("create")]
        [Produces("application/json")]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User is null");
            }

            var newUser = await _usersService.CreateUserAsync(user);

            if (newUser == null)
            {
                return NotFound();
            }

            return CreatedAtRoute(nameof(GetUserById), new { id = newUser.UserId }, newUser);
        }

        [HttpGet("get/id/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            if (id < 0)
            {
                return BadRequest("ID cannot be < 0");
            }

            var response = await _usersService.GetUserByIdAsync(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpGet("get/email/{email}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("String is null or empty");
            }

            var response = await _usersService.GetUserByEmailAsync(email);
            
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("get/name/{name}")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByName([FromRoute] string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest("String is null or empty");
            }

            var response = await _usersService.GetUserByNameAsync(name);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpGet("get/all")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAllUsers()
        {
            var response = await _usersService.GetAllUsersAsync();

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        [HttpDelete("delete/{userId}")]
        [Produces("application/json")]
        public async Task<IActionResult> DeleteUserById([FromQuery] int userId)
        {
            if (userId < 0)
            {
                return BadRequest("UserID cannot be < 0");
            }

            var response = await _usersService.DeleteUserByIdAsync(userId);

            return Ok(response);
        }

    }
}
