using Backend.DTO.TransactionsDTO;
using Backend.Interfaces.ITransactions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.TransactionController
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {

        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionTicketsAsync()
        {
            var transaction = await _transactionService.GetAllTransactionTicketsAsync();
            return Ok(transaction);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetTransactionTicketsByUserIdAsync(int userId)
        {
            var transaction = await _transactionService.GetTransactionTicketsByUserIdAsync(userId);
            if (!transaction?.Any() ?? true)
            {
                return NotFound($"No transaction, with relation user id {userId}, was found.");

            }

            return Ok(transaction);

        }

        [HttpPost("createticket")]
        public async Task<IActionResult> CreateTransactionTicket([FromForm] TransactionRequest request)
        {
            var createdTransaction = await _transactionService.CreateTransactionTicket(request);
            return Ok($"Transaction with id {createdTransaction.TransactionId} and with userId {createdTransaction.UserId}, was successfully created.");

        }




    }
}
