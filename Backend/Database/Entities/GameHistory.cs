using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class GameHistory
    {
        [Key]
        public Guid GameHistoryId { get; set; } = Guid.NewGuid();

        public DateTime Date {  get; set; }

        [Column(TypeName = "int")]
        public required int UserId { get; set; }
        
        [Column(TypeName = "int")]
        public required int GameId { get; set; } 

        [Column(TypeName = "decimal(18,2)")]
        public required decimal BetAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal WinAmount { get; set; }

        public required bool IsWin { get; set; } = false;
        public bool? WasCashedOut { get; set; } = false;


        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
    }
}
