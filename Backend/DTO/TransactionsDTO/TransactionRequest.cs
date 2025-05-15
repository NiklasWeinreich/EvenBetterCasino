using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.TransactionsDTO
{
    public class TransactionRequest
    {

        public int UserId { get; set; }

        public decimal Amount { get; set; }

        public string Type { get; set; }

    }
}
