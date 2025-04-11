using Backend.Database.Entities;

namespace Backend.Interfaces.IUser
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User> CreateUserAsync(User newUser);
        Task<User> UpdateUserAsync(User updateUser);
        Task<bool> DeleteUserAsync(int id);
        Task<User> GetUserByEmail(string email);
    }
}

