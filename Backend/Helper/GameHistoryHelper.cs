using Backend.DTO.GamesHistoryDTO;
using Backend.Interfaces.IGamesHistory;

namespace Backend.Helper
{
    public class GameHistoryHelper
    {

        private readonly IGameHistoryService _gameHistoryService;

        public GameHistoryHelper(IGameHistoryService gameHistoryService)
        {
            _gameHistoryService = gameHistoryService;
        }

        public async Task LogGameWithoutCashOut(int userId, int gameId, decimal betAmount, decimal winAmount, bool isWin)
        {
            var request = new GameHistoryRequest
            {
                UserId = userId,
                GameId = gameId,
                BetAmount = betAmount,
                WinAmount = winAmount,
                IsWin = isWin,
                WasCashedOut = null, // eksplicit!
            };

            await _gameHistoryService.CreateGameHistoryTicketAsync(request);
        }


        public async Task LogCashOutGameAsync(int userId, int gameId, decimal betAmount, decimal winAmount, bool isWin, bool wasCashedOut, bool isJackpotWin = false)
        {
            var request = new GameHistoryRequest
            {
                UserId = userId,
                GameId = gameId,
                BetAmount = betAmount,
                WinAmount = winAmount,
                IsWin = isWin,
                WasCashedOut = wasCashedOut,
            };

            await _gameHistoryService.CreateGameHistoryTicketAsync(request);
        }

    }
}
