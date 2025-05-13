using Backend.Database.Entities;
using Backend.DTO.GamesDTO;
using Backend.DTO.GamesHistoryDTO;
using Backend.Interfaces.IGamesHistory;
using Backend.Repositories.GamesHistoryRepository;

namespace Backend.Services.GamesHistoryService
{
    public class GameHistoryService : IGameHistoryService
    {

        private readonly IGameHistoryRepository _gameHistoryRepository;

        public GameHistoryService(IGameHistoryRepository gameHistoryRepository)
        {
            _gameHistoryRepository = gameHistoryRepository;
        }

        public async Task<List<GameHistoryResponse>> GetAllGameHistoryTicketsAsync()
        {
            var ticket = await _gameHistoryRepository.GetAllGameHistoryTicketsAsync();

            return ticket.Select(MapEntityToResponse).ToList();
        }
        public async Task<GameHistoryResponse?> GetGameHistoryByUserIdAsync(int userId)
        {

            var ticket = await _gameHistoryRepository.GetGameHistoryByUserIdAsync(userId);
            if (ticket == null) return null;

            return MapEntityToResponse(ticket);


        }

        public async Task<GameHistoryResponse?> GetGameHistoryByGameIdAsync(int gameId)
        {
            var ticket = await _gameHistoryRepository.GetGameHistoryByGameIdAsync(gameId);
            if (ticket == null) return null;

            return MapEntityToResponse(ticket);
        }
        public async Task<GameHistoryResponse> CreateGameHistoryTicket(GameHistoryRequest request)
        {


            var newTicket = new GameHistory
            {
                UserId = request.UserId,
                GameId = request.GameId,
                BetAmount = request.BetAmount,
                IsWin = request.IsWin,
                IsJackpotWin = request.IsJackpotWin,
                Date = DateTime.UtcNow
            };

            var createdTicket = await _gameHistoryRepository.CreateGameHistoryTicket(newTicket);

            return MapEntityToResponse(createdTicket);
        }

        public static GameHistoryResponse MapEntityToResponse(GameHistory response)
        {
            return new GameHistoryResponse
            {

                GameHistoryId = response.GameHistoryId,
                GameId = response.GameId,
                UserId = response.UserId,
                BetAmount = response.BetAmount,
                IsJackpotWin = response.IsJackpotWin,
                IsWin = response.IsWin,
                Date = response.Date,


            };
        }

    }
}
