using Backend.Database.Entities;

namespace Backend.Interfaces.ITransactions
{
    public interface ITransactionRepository
    {

        Task<List<Transaction>> GetAllTransactionTicketsAsync();

        Task<List<Transaction>> GetTransactionTicketsByUserIdAsync(int id);


        Task<Transaction> CreateTransactionTicket(Transaction transaction);


    }
}
