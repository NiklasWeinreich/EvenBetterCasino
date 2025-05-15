using Backend.Database.DatabaseContext;
using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;
using Backend.Interfaces.IGamesHistory;

namespace Backend.Repositories.GamesHistoryRepository
{
    public class GameHistoryRepository : IGameHistoryRepository
    {

        private readonly DatabaseContext _databaseContext;

        public GameHistoryRepository(DatabaseContext databaseContext) 
        { 
            _databaseContext = databaseContext;
        }


        public async Task<List<GameHistory>> GetAllGameHistoryTicketsAsync()
        {
            return await _databaseContext.GameHistories
                .Include(g => g.User)
                .Include(g => g.Game)
                .ToListAsync();

        }
        public async Task<List<GameHistory>> GetGameHistoryByUserIdAsync(int id)
        {
            return await _databaseContext.GameHistories
                .Where(g => g.User.Id == id)
                .Include(g => g.User)
                .Include(g => g.Game)
                .ToListAsync();

        }

        public async Task<List<GameHistory>> GetGameHistoryByGameIdAsync(int id)
        {
            return await _databaseContext.GameHistories
                .Where(g => g.Game.GameId == id)
                .Include(g => g.User)
                .Include(g => g.Game)
                .OrderByDescending(g => g.Date)
                .ToListAsync();
        }
        public async Task<List<GameHistory>> GetGameHistoryByGameIdAndUserIdAsync(int userId, int gameId)
        {
            return await _databaseContext.GameHistories
                .Where(g => g.GameId == gameId && g.UserId == userId)
                .Include(g => g.User)
                .Include(g => g.Game)
                .OrderByDescending(g => g.Date)
                .ToListAsync();
        }

        public async Task<GameHistory> CreateGameHistoryTicketAsync(GameHistory ticket)
        {

            _databaseContext.GameHistories.Add(ticket);
            await _databaseContext.SaveChangesAsync();

            return ticket;
        }

    }
}
