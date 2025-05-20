using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Backend.DTO.GamesHistoryDTO
{
    public class GameHistoryResponse
    {
        public Guid GameHistoryId { get; set; }

        public DateTime Date { get; set; }
        public string DateFormatted => Date.ToString("dd/MM/yyyy HH:mm:ss", new CultureInfo("da-DK"));

        public required int UserId { get; set; }

        public string? UserName { get; set; }

        public required int GameId { get; set; }

        public string? GameName { get; set; }

        public required decimal BetAmount { get; set; }
        public decimal WinAmount { get; set; }

        public required bool IsWin { get; set; } = false;
        public bool? WasCashedOut { get; set; }
    }
}
