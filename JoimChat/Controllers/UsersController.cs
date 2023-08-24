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

            await _usersService.CreateUserAsync(user);

            return CreatedAtRoute(nameof(GetUserById), new { id = user.UserId }, user);
        }

        [HttpGet("get/userId")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserById([FromQuery] int userId)
        {
            if (userId < 0)
            {
                return BadRequest("ID cannot be < 0");
            }
            
            var response = await _usersService.GetUserByIdAsync(userId);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }


        [HttpGet("get/email")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
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

        [HttpGet("get/name")]
        [Produces("application/json")]
        public async Task<IActionResult> GetUserByName([FromQuery] string name)
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

            return Ok(await _usersService.DeleteUserByIdAsync(userId));
        }

        [HttpPut("update/{userId}")]
        [Produces("application/json")]
        public async Task<IActionResult> UpdateUser([FromQuery] int userId,[FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest();
            }

            return Ok(await _usersService.UpdateUserAsync(userId, user));
        }

    }
}
