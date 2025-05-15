using Azure;
using Backend.Database.Entities;
using Backend.DTO.TransactionsDTO;
using Backend.Interfaces.ITransactions;
using Backend.Interfaces.IUser;

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

            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"User with ID {request.UserId} does not exist.");


            var newTransaction = new Transaction
            {
                UserId = request.UserId,
                Amount = request.Amount,
                Type = request.Type,
                Direction = request.Direction
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
                Direction = response.Direction,

                Date = DateTime.UtcNow

            };
        }
    }
}
