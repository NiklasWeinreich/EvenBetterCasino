using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class Transactions
    {

        [Key]
        public int Id { get; set; }

        [Column(TypeName = "int")]        
        public int UserId { get; set; }
        
        [Column(TypeName = "int")]
        public int Amount { get; set; }
        
        public DateOnly Date { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string Direction { get; set; }

    }
}
