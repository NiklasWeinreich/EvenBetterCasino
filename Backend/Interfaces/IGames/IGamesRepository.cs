using Backend.Database.Entities;

namespace Backend.Interfaces.IGames
{
    public interface IGamesRepository
    {

        Task<List<Game>> GetAllGamesAsync();
        Task<Game?> GetGameByIdAsync(int id);
        Task<Game> CreateGameAsync(Game newGame);
        Task<Game> UpdateGameAsync(Game updateGame);
        Task<bool> DeleteGameAsync(int id);



    }
}
