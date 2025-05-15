using Azure;
using Backend.Database.Entities;
using Backend.DTO.TransactionsDTO;
using Backend.Helper;
using Backend.Interfaces.ITransactions;
using Backend.Interfaces.IUser;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend.Services.TransactionsService
{
    public class TransactionService : ITransactionService
    {

        private readonly ITransactionRepository _transactionRepository;
        private readonly IUserService _userService;

        public TransactionService(ITransactionRepository transactionRepository, IUserService userService)
        {
            _transactionRepository = transactionRepository;
            _userService = userService;

        }

        public async Task<List<TransactionResponse>> GetAllTransactionTicketsAsync()
        {
            var transaction = await _transactionRepository.GetAllTransactionTicketsAsync();

            return transaction.Select(MapEntityToResponse).ToList();
        }

        public async Task<List<TransactionResponse>> GetTransactionTicketsByUserIdAsync(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null) throw new ArgumentException($"User with ID {id} does not exist.");

            var transaction = await _transactionRepository.GetTransactionTicketsByUserIdAsync(id);

            return transaction.Select(MapEntityToResponse).ToList();


        }
        public async Task<TransactionResponse> CreateTransactionTicket(TransactionRequest request)
        {

            if (request.Type != TransactionsHelper.TransactionTypes.Deposit && request.Type != TransactionsHelper.TransactionTypes.Withdrawal)
                throw new ArgumentException("Incoming transactions must have 'Indbetaling' or 'Udbetaling' as input.");
            
            if (request.Amount < 50)
                throw new ArgumentException("Amount must be greater than 50.");

            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"User with ID {request.UserId} does not exist.");


            var newTransaction = new Transaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Type = request.Type,
                Date = DateTime.UtcNow
            };


            var createdTransaction = await _transactionRepository.CreateTransactionTicket(newTransaction);

            return MapEntityToResponse(createdTransaction);

        }

        public static TransactionResponse MapEntityToResponse(Transaction response)
        {
            return new TransactionResponse
            {

                TransactionId = response.TransactionId,
                UserId = response.UserId,
                UserName = response.User?.FirstName + " " + response.User?.LastName,
                Amount = response.Amount,
                Type = response.Type,

                Date = response.Date,

            };
        }
    }
}
