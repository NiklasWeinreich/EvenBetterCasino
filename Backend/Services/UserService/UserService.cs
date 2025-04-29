using Backend.Authentication;
using Backend.Database.Entities;
using Backend.DTO.LoginDTO;
using Backend.DTO.UserDTO;
using Backend.Interfaces.IUser;

namespace Backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest newUserRequest)
        {
            var newUser = MapRequestToEntity(newUserRequest);
            var createdUser = await _userRepository.CreateUserAsync(newUser);
            return MapEntityToResponse(createdUser);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var deletedUser = await _userRepository.DeleteUserAsync(id);
            return deletedUser;
        }

        public async Task<List<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllUserAsync();
            return users.Select(MapEntityToResponse).ToList();
        }

        public async Task<UserResponse?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user != null)
            {
                return MapEntityToResponse(user);
            }

            return null;
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
                return null;

            return MapEntityToResponse(user);
        }

        public async Task<UserResponse> UpdateUserAsync(int id, UserRequest updateUser)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(id);
            if (existingUser == null)
                throw new Exception("User not found");

            existingUser.FirstName = updateUser.FirstName;
            existingUser.LastName = updateUser.LastName;

            // Undgå at overskrive password med null
            if (!string.IsNullOrWhiteSpace(updateUser.Password))
            {
                existingUser.Password = updateUser.Password!;
            }

            existingUser.Email = updateUser.Email;
            existingUser.BirthDate = updateUser.BirthDate;
            existingUser.PhoneNumber = updateUser.PhoneNumber;
            existingUser.NewsLetterIsSubscribed = updateUser.NewsLetterIsSubscribed;
            existingUser.Role = updateUser.Role;

            var updatedUser = await _userRepository.UpdateUserAsync(existingUser);
            return MapEntityToResponse(updatedUser);
        }


        public User MapRequestToEntity(UserRequest userRequest)
        {
            var user = new User
            {
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Password = userRequest.Password!,
                Email = userRequest.Email,
                BirthDate = userRequest.BirthDate,
                PhoneNumber = userRequest.PhoneNumber,
                NewsLetterIsSubscribed = userRequest.NewsLetterIsSubscribed,
                Role = userRequest.Role,

                Balance = 0,
                Profit = 0,
                Loss = 0,
                ExcludedUntil = null
            };

            return user;
        }

        public static UserResponse MapEntityToResponse(User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                //Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                BirthDate = user.BirthDate,
                NewsLetterIsSubscribed = user.NewsLetterIsSubscribed,
                Balance = user.Balance,
                Profit = user.Profit,
                Loss = user.Loss,
                ExcludedUntil = user.ExcludedUntil,
                Role = user.Role,
            };
        }


        public async Task<LoginResponse?> AuthenticateUserAsync(LoginRequest loginRequest)
        {
            User user = await _userRepository.GetUserByEmail(loginRequest.Email);
            if (user == null)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.Password))
            {
                LoginResponse response = new()
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = user.Role,
                    Token = _jwtUtils.GenerateJwtToken(user)
                };
                return response;
            }
            return null;
        }

        public async Task<UserResponse?> ExcludeUserAsync(int id, int exclusionPeriodHours)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null) return null;


            user.ExcludedUntil = DateTime.UtcNow.AddHours(exclusionPeriodHours);
            await _userRepository.UpdateUserAsync(user);

            return UserService.MapEntityToResponse(user);
        }

    }
}
