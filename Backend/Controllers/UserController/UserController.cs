using Backend.DTO.UserDTO;
using Backend.Interfaces.IUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.UserController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public UserController(IUserService userService, IUserRepository userRepository)
        {
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task <ActionResult<List<UserResponse>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound("Brugeren blev ikke fundet!"); 
            }

            return Ok(user);
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> CreateUserAsync([FromForm] UserRequest request)
        {
            try
            {
                var mail = await _userService.GetUserByEmailAsync(request.Email);
                if (mail != null)
                {
                    return Conflict("Email is already in use");
                }

                UserResponse userResponse = await _userService.CreateUserAsync(request);

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<ActionResult<UserResponse>> UpdateUser([FromRoute] int id, [FromForm] UserRequest request)
        {
            var updatedUser = await _userService.UpdateUserAsync(id, request);

            if (updatedUser == null)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User updated successfully", user = updatedUser });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            var isDeleted = await _userService.DeleteUserAsync(id);

            if (!isDeleted)
                return NotFound(new { message = "User not found" });

            return Ok(new { message = "User deleted successfully" });
        }

    }
}
