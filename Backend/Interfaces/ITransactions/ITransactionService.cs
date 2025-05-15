using Backend.DTO.TransactionsDTO;

namespace Backend.Interfaces.ITransactions
{
    public interface ITransactionService
    {

        Task<List<TransactionResponse>> GetAllTransactionTicketsAsync();

        Task<List<TransactionResponse>> GetTransactionTicketsByUserIdAsync(int id);

        Task<TransactionResponse> CreateTransactionTicket(TransactionRequest request);


    }
}
