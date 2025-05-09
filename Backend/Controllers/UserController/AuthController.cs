using Backend.DTO.LoginDTO;
using Backend.DTO.UserDTO;
using Backend.Interfaces.IUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var response = await _userService.AuthenticateUserAsync(loginRequest);
            return response == null ? Unauthorized("Invalid credentials") : Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserResponse>> RegisterUser([FromBody] UserRequest newUser)
        {
            var existingUser = await _userService.GetUserByEmailAsync(newUser.Email);
            if (existingUser != null)
            {
                return Conflict("Email is already in use");
            }

            var userResponse = await _userService.CreateUserAsync(newUser);
            return Ok(userResponse);
        }
    }
}

