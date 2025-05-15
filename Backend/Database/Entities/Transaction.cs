using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class Transaction
    {

        [Key]
        public Guid TransactionId { get; set; } = Guid.NewGuid();

        [Column(TypeName = "int")]        
        public int UserId { get; set; }
        
        [Column(TypeName = "decimal")]
        public decimal Amount { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Type { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

    }
}
