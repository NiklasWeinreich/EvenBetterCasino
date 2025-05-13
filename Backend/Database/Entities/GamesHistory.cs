using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class GamesHistory
    {
        [Key]
        public int GamesHistoryId { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime Date {  get; set; }

        [Column(TypeName = "int")]
        public required int UserId { get; set; }
        
        [Column(TypeName = "int")]
        public required int GamesId { get; set; }

        [Column(TypeName = "decimal")]
        public required decimal BetAmount { get; set; }
        
        public required bool IsWin { get; set; } = false;
        
        public required bool IsJackpotWin { get; set; } = false;

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
