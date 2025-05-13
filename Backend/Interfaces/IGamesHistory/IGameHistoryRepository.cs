using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;

namespace Backend.Interfaces.IGamesHistory
{
    public interface IGameHistoryRepository
    {

        // Get All
        Task<List<GameHistory>> GetAllGameHistoryTicketsAsync();

        // Get BY USER ID 
        Task<GameHistory?> GetGameHistoryByUserIdAsync(int id);

        // Get BY GAME ID
        Task<GameHistory?> GetGameHistoryByGameIdAsync(int id);


        // USER ID && DATO

        // USER ID && DATO

        // CREATE GH
        Task<GameHistory> CreateGameHistoryTicket(GameHistory ticket);





    }
}
