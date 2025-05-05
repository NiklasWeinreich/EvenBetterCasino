using Backend.Database.DatabaseContext;
using Backend.Interfaces.IBalance;

namespace Backend.Repositories.BalanceRepository
{
    public class BalanceRepository : IBalanceRepository
    {

        private readonly DatabaseContext _databaseContext;

        public BalanceRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<decimal> GetBalanceAsync(int userId)
        {
            var user = await _databaseContext.Users.SingleOrDefaultAsync(u => u.Id == userId);
            if (user == null) throw new ArgumentException("User is not found");

            return user.Balance;
        }
        public async Task UpdateBalanceAsync(int userId, decimal newBalance)
        {
            var currentUser = _databaseContext.Users.SingleOrDefault(u => u.Id == userId);
            if (currentUser == null) throw new ArgumentException("User is not found");

            currentUser.Balance = newBalance;
            await _databaseContext.SaveChangesAsync();
        }
    }
}
