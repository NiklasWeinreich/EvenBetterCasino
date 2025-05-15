using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces.IGamesHistory
{
    public interface IGameHistoryService
    {

        // GET ALL
        Task<List<GameHistoryResponse>> GetAllGameHistoryTicketsAsync();

        // Get BY USER ID 
        Task<List<GameHistoryResponse>> GetGameHistoryByUserIdAsync(int userId);

        // Get BY GAME ID
        Task<List<GameHistoryResponse>> GetGameHistoryByGameIdAsync(int gameId);


        // USER ID && GAME ID
        Task<List<GameHistoryResponse>> GetGameHistoryByGameIdAndUserIdAsync(int userId, int gameId);


        // USER ID && DATO

        // CREATE GH
        Task<GameHistoryResponse> CreateGameHistoryTicketAsync(GameHistoryRequest ticket);
    }
}
