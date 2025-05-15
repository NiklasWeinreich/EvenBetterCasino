using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.TransactionsDTO
{
    public class TransactionRequest
    {

        public int UserId { get; set; }

        public int Amount { get; set; }

        public string Type { get; set; }

        public string Direction { get; set; }


    }
}
