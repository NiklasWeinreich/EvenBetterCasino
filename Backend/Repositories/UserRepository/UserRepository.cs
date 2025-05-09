using Backend.Database.DatabaseContext;
using Backend.Database.Entities;
using Backend.Interfaces.IUser;

namespace Backend.Repositories.UserRepository
{
    public class UserRepository : IUserRepository 
    {
        private readonly DatabaseContext _dbcontext;

        public UserRepository(DatabaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public async Task<User> CreateUserAsync(User newUser)
        {
            //newUser.Password = BCrypt.Net.BCrypt.HashPassword(newUser.Password);
            
            _dbcontext.Users.Add(newUser);
            await _dbcontext.SaveChangesAsync();
            return newUser;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var existingUser = await _dbcontext.Users.FindAsync(id);
            if (existingUser == null)
                return false;

            _dbcontext.Users.Remove(existingUser);
            await _dbcontext.SaveChangesAsync();

            return true;
        }

        public async Task<User?> ExcludeUserAsync(int Id, int exclusionPeriodHours)
        {
            var user = await GetUserByIdAsync(Id);
            if (user == null) return null;

            user.ExcludedUntil = DateTime.UtcNow.AddHours(exclusionPeriodHours);
            await UpdateUserAsync(user);

            return user;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _dbcontext.Users.ToListAsync();
        }

        public Task<List<User>> GetNewsletterSubscribersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _dbcontext.Users.FindAsync(id);
        }

        public async Task<User?> SubscribeNewsletter(string email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            user.NewsLetterIsSubscribed = true;
            return await UpdateUserAsync(user);
        }


        public async Task<User> UpdateUserAsync(User updateUser)
        {
            var existingUser = await _dbcontext.Users.FindAsync(updateUser.Id);
            if (existingUser == null)
                return null;

            _dbcontext.Entry(existingUser).CurrentValues.SetValues(updateUser);

            await _dbcontext.SaveChangesAsync();
            return existingUser;
        }

        public async Task<User?> UnsubscribeNewsletter(string email)
        {
            var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return null;

            user.NewsLetterIsSubscribed = false;
            return await UpdateUserAsync(user);
        }

    }
}

