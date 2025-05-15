using Backend.Database.DatabaseContext;
using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;
using Backend.Interfaces.IGames;
using Backend.Interfaces.IGamesHistory;
using Backend.Interfaces.IUser;
using Backend.Services.GamesService;

namespace Backend.Services.GamesHistoryService
{
    public class GameHistoryService : IGameHistoryService
    {

        private readonly IGameHistoryRepository _gameHistoryRepository;
        private readonly IUserService _userService;
        private readonly IGamesService _gamesService;

        public GameHistoryService(IGameHistoryRepository gameHistoryRepository, IUserService userService, IGamesService gamesService)
        {
            _gameHistoryRepository = gameHistoryRepository;
            _userService = userService;
            _gamesService = gamesService;
        }


        public async Task<List<GameHistoryResponse>> GetAllGameHistoryTicketsAsync()
        {
            var ticket = await _gameHistoryRepository.GetAllGameHistoryTicketsAsync();

            return ticket.Select(MapEntityToResponse).ToList();
        }


        public async Task<List<GameHistoryResponse>> GetGameHistoryByUserIdAsync(int userId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID {userId} does not exist.");

            var tickets = await _gameHistoryRepository.GetGameHistoryByUserIdAsync(userId);

            return tickets.Select(MapEntityToResponse).ToList();

        }


        public async Task<List<GameHistoryResponse>> GetGameHistoryByGameIdAsync(int gameId)
        {

            var game = await _gamesService.GetGameByIdAsync(gameId);
            if (game == null) throw new ArgumentException($"Game with ID {gameId} does not exist.");

            var tickets = await _gameHistoryRepository.GetGameHistoryByGameIdAsync(gameId);

            return tickets.Select(MapEntityToResponse).ToList();
        }


        public async Task<List<GameHistoryResponse>> GetGameHistoryByGameIdAndUserIdAsync(int userId, int gameId)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null) throw new ArgumentException($"User with ID {userId} does not exist.");

            // Tjek om spillet eksisterer
            var game = await _gamesService.GetGameByIdAsync(gameId);
            if (game == null) throw new ArgumentException($"Game with ID {gameId} does not exist.");


            var tickets = await _gameHistoryRepository.GetGameHistoryByGameIdAndUserIdAsync(userId, gameId);



            return tickets.Select(MapEntityToResponse).ToList();

        }


        public async Task<GameHistoryResponse> CreateGameHistoryTicketAsync(GameHistoryRequest request)
        {

            var user = await _userService.GetUserByIdAsync(request.UserId);
            if (user == null) throw new ArgumentException($"User with ID {request.UserId} does not exist.");
            

            var game = await _gamesService.GetGameByIdAsync(request.GameId);
            if (game == null) throw new ArgumentException($"Game with ID {request.GameId} does not exist.");

            if (!request.IsWin && (request.WinAmount > 0 || request.WasCashedOut == true))
                throw new ArgumentException("WinAmount and WasCashedOut can only be set if IsWin is true.");


            var newTicket = new GameHistory
            {
                UserId = request.UserId,
                GameId = request.GameId,
                BetAmount = request.BetAmount,
                WinAmount = request.WinAmount,
                IsWin = request.IsWin,
                WasCashedOut = request.WasCashedOut,
                Date = DateTime.UtcNow
            };

            var createdTicket = await _gameHistoryRepository.CreateGameHistoryTicketAsync(newTicket);

            return MapEntityToResponse(createdTicket);
        }

        public static GameHistoryResponse MapEntityToResponse(GameHistory response)
        {
            return new GameHistoryResponse
            {

                GameHistoryId = response.GameHistoryId,
                GameId = response.GameId,
                GameName = response.Game?.Name,
                UserId = response.UserId,
                UserName = response.User?.FirstName + " " + response.User?.LastName,
                BetAmount = response.BetAmount,
                WinAmount = response.WinAmount,
                IsWin = response.IsWin,
                WasCashedOut = response.WasCashedOut,
                Date = response.Date,


            };
        }

    }
}
