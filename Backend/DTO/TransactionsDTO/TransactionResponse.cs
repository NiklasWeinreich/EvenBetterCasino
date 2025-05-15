using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.TransactionsDTO
{
    public class TransactionResponse
    {

        public Guid TransactionId { get; set; }

        public int UserId { get; set; }

        public string? UserName { get; set; }

        public int Amount { get; set; }

        public string Type { get; set; }

        public DateTime Date { get; set; }

        public string Direction { get; set; }

    }
}
