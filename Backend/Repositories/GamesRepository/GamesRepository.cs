using Backend.Database.DatabaseContext;
using Backend.Database.Entities;
using Backend.Interfaces.IGames;

namespace Backend.Repositories.GamesRepository
{
    public class GamesRepository : IGamesRepository
    {

        private readonly DatabaseContext _databaseContext;

        public GamesRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        
        public async Task<List<Game>> GetAllGamesAsync()
        {
            return await _databaseContext.Games.ToListAsync();
        }

        public async Task<Game?> GetGameByIdAsync(int id)
        {
            return await _databaseContext.Games.FindAsync(id);
        }

        public async Task<Game> CreateGameAsync(Game newGame)
        {

            _databaseContext.Games.Add(newGame);
            await _databaseContext.SaveChangesAsync();

            return newGame;

        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var existingGame = await _databaseContext.Games.FindAsync(id);
            if (existingGame == null) return false;

            _databaseContext.Games.Remove(existingGame);
            await _databaseContext.SaveChangesAsync();

            return true;

        }

        public async Task<Game> UpdateGameAsync(Game updateGame)
        {
            var existingGame = await _databaseContext.Games.FindAsync(updateGame.Id);
            if (existingGame == null) return null;

            _databaseContext.Entry(existingGame).CurrentValues.SetValues(updateGame);

            await _databaseContext.SaveChangesAsync();
            return existingGame;
        }
    }
}
