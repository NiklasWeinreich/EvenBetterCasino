using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class GamesHistory
    {
        [Key]
        public int Id { get; set; }

        public DateOnly Date {  get; set; }

        [Column(TypeName = "int")]
        public required int UserId { get; set; }
        
        [Column(TypeName = "int")]
        public required int GamesId { get; set; }

        [Column(TypeName = "int")]
        public required int BetAmount { get; set; }
        
        [Column(TypeName = "bool")]
        public required bool Win { get; set; } = false;
        
        [Column(TypeName = "bool")]
        public required bool JackpotWin { get; set; } = false;
    }
}
