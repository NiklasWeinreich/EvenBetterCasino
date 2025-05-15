using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.GamesHistoryDTO

{
    public class GameHistoryRequest
    {
 
        public required int UserId { get; set; }
        public required int GameId { get; set; }
        public required decimal BetAmount { get; set; }
        public decimal WinAmount { get; set; }
        public required bool IsWin { get; set; } = false;
        public bool? WasCashedOut { get; set; }
    }
}
