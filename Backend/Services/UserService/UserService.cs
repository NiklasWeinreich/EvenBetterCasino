using Backend.Authentication;
using Backend.Database.Entities;
using Backend.DTO.EmailDTO;
using Backend.DTO.LoginDTO;
using Backend.DTO.UserDTO;
using Backend.Interfaces.IEmail;
using Backend.Interfaces.IUser;

namespace Backend.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtUtils _jwtUtils;
        private readonly IEmailService _emailService;

        public UserService(IUserRepository userRepository, IJwtUtils jwtUtils, IEmailService emailService)
        {
            _userRepository = userRepository;
            _jwtUtils = jwtUtils;
            _emailService = emailService;
        }

        public async Task<UserResponse> CreateUserAsync(UserRequest newUserRequest)
        {
            var newUser = MapRequestToEntity(newUserRequest);
            var createdUser = await _userRepository.CreateUserAsync(newUser);

            if (newUser.NewsLetterIsSubscribed)
            {
                await SubscribeNewsletter(newUser.Email);
            }

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
                existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateUser.Password!);
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
                Password = BCrypt.Net.BCrypt.HashPassword(userRequest.Password!),
                Email = userRequest.Email,
                BirthDate = userRequest.BirthDate,
                PhoneNumber = userRequest.PhoneNumber,
                NewsLetterIsSubscribed = userRequest.NewsLetterIsSubscribed,
                Role = userRequest.Role,

                Balance = 0,
                Profit = 0,
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

        public async Task<UserResponse?> SubscribeNewsletter(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return null;

            if (!user.NewsLetterIsSubscribed)
            {
                user.NewsLetterIsSubscribed = true;
                await _userRepository.UpdateUserAsync(user);

                var mail = new EmailResponse
                {
                    To = user.Email,
                    Subject = "Velkommen til EvenBetterCasino nyhedsbrevet!",
                    Body = $"Hej {user.FirstName}!<br><br>" +
                           "Tak fordi du har tilmeldt dig vores nyhedsbrev.<br>" +
                           "Du vil nu modtage spændende opdateringer, nyheder og tilbud direkte i din indbakke.<br><br>" +
                           "Hilsen<br>EvenBetter Teamet"
                };

                _emailService.SendEmail(mail);
            }

            return MapEntityToResponse(user);
        }

        public async Task<UserResponse?> UnsubscribeNewsletter(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
                return null;

            if (user.NewsLetterIsSubscribed)
            {
                user.NewsLetterIsSubscribed = false;
                await _userRepository.UpdateUserAsync(user);

                var mail = new EmailResponse
                {
                    To = user.Email,
                    Subject = "Afmeldt fra nyhedsbrev",
                    Body = $"Hej {user.FirstName}!<br><br>" +
                           "Du er nu afmeldt vores nyhedsbrev.<br><br>" +
                           "Hilsen<br>EvenBetter Teamet"
                };

                _emailService.SendEmail(mail);
            }

            return MapEntityToResponse(user);
        }



    }
}
