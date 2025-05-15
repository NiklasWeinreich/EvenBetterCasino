using Backend.DTO.GamesDTO;

namespace Backend.Interfaces.IGames
{
    public interface IGamesService
    {

        Task<List<GamesResponse>> GetAllGamesAsync();
        Task<GamesResponse?> GetGameByIdAsync(int id);
        Task<GamesResponse> CreateGameAsync(GamesRequest newGameRequest);
        Task<GamesResponse> UpdateGameAsync(int id, GamesRequest updatedGame);
        Task<bool> DeleteGameAsync(int id);

    }
}
