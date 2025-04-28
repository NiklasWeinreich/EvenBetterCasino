using Backend.DTO.LoginDTO;
using Backend.DTO.UserDTO;

namespace Backend.Interfaces.IUser
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task<UserResponse?> GetUserByIdAsync(int id);
        Task<UserResponse?> GetUserByEmailAsync(string email);
        Task<UserResponse> CreateUserAsync(UserRequest newUserRequest);
        Task<UserResponse> UpdateUserAsync(int id, UserRequest updateUser);
        Task<bool> DeleteUserAsync(int id);
        Task<LoginResponse?> AuthenticateUserAsync(LoginRequest loginRequest);
    }
}
