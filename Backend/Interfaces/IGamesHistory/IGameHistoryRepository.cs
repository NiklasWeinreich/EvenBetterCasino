using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;

namespace Backend.Interfaces.IGamesHistory
{
    public interface IGameHistoryRepository
    {

        // Get All
        Task<List<GameHistory>> GetAllGameHistoryTicketsAsync();

        // Get BY USER ID 
        Task<List<GameHistory>> GetGameHistoryByUserIdAsync(int id);

        // Get BY GAME ID
        Task<List<GameHistory>> GetGameHistoryByGameIdAsync(int id);


        // USER ID && GAME ID
        Task<List<GameHistory>> GetGameHistoryByGameIdAndUserIdAsync(int userId, int gameId);

        // USER ID && DATO

        // CREATE GH
        Task<GameHistory> CreateGameHistoryTicketAsync(GameHistory ticket);





    }
}
