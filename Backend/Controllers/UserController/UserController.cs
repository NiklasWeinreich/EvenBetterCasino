using Backend.DTO.UserDTO;
using Backend.Interfaces.IUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Backend.Authentication.Authentication;

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

        [HttpPost("{id}/exclude")]
        public async Task<IActionResult> ExcludeUserAsync([FromRoute] int id, [FromBody] int exclusionPeriodHours)
        {
            try
            {
                var userResponse = await _userService.ExcludeUserAsync(id, exclusionPeriodHours);

                if (userResponse == null)
                {
                    return NotFound(new { message = "User not found." });
                }

                return Ok(userResponse);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Newsletter/Subscribe/{email}")]
        public async Task<IActionResult> SubscribeNewsletter([FromRoute] string email)
        {
            try
            {
                UserResponse userResponse = await _userService.SubscribeNewsletter(email);

                if (userResponse != null)
                {
                    return Ok(userResponse);
                }
                return Problem();
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPost]
        [Route("Newsletter/Unsubscribe/{email}")]
        public async Task<IActionResult> UnsubscribeNewsletter([FromRoute] string email)
        {
            try
            {
                UserResponse userResponse = await _userService.UnsubscribeNewsletter(email);

                if (userResponse != null)
                {
                    return Ok(userResponse);
                }
                return NotFound("User not found or already unsubscribed.");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }


    }
}
