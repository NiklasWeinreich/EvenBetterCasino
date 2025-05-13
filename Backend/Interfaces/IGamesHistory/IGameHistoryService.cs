using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;

namespace Backend.Interfaces.IGamesHistory
{
    public interface IGameHistoryService
    {

        // GET ALL
        Task<List<GameHistoryResponse>> GetAllGameHistoryTicketsAsync();

        // Get BY USER ID 
        Task<GameHistoryResponse?> GetGameHistoryByUserIdAsync(int userId);

        // Get BY GAME ID
        Task<GameHistoryResponse?> GetGameHistoryByGameIdAsync(int gameId);


        // USER ID && DATO

        // USER ID && DATO

        // CREATE GH
        Task<GameHistoryResponse> CreateGameHistoryTicket(GameHistoryRequest newGameHistoryTicket);
    }
}
