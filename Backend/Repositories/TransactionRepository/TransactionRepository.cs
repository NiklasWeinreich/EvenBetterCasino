using Backend.Database.DatabaseContext;
using Backend.Database.Entities;
using Backend.DTO.TransactionsDTO;
using Backend.Interfaces.ITransactions;

namespace Backend.Repositories.TransactionsRepository
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly DatabaseContext _databaseContext;

        public TransactionRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<List<Transaction>> GetAllTransactionTicketsAsync()
        {
            return await _databaseContext.Transactions
                .Include(g => g.User)
                .ToListAsync();
        }

        public async Task<List<Transaction>> GetTransactionTicketsByUserIdAsync(int id)
        {
            return await _databaseContext.Transactions
                .Where(g => g.UserId == id)
                .Include(g => g.User)
                .ToListAsync();
        }
        public async Task<Transaction> CreateTransactionTicket(Transaction transaction)
        {
            _databaseContext.Transactions.Add(transaction);
            await _databaseContext.SaveChangesAsync();
            return transaction;
        }
    }
}
