using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class GameHistory
    {
        [Key]
        public int GameHistoryId { get; set; }

        public DateTime Date {  get; set; }

        [Column(TypeName = "int")]
        public required int UserId { get; set; }
        
        [Column(TypeName = "int")]
        public required int GameId { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public required decimal BetAmount { get; set; }
        
        public required bool IsWin { get; set; } = false;
        
        public required bool IsJackpotWin { get; set; } = false;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
