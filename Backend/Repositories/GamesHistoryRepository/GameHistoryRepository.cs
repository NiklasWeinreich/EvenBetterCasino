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
                .Include(gh => gh.User)
                .Include(gh => gh.Game)
                .ToListAsync();

        }
        public async Task<GameHistory?> GetGameHistoryByGameIdAsync(int id)
        {
            return await _databaseContext.GameHistories.FindAsync(id);

        }

        public async Task<GameHistory?> GetGameHistoryByUserIdAsync(int id)
        {
            return await _databaseContext.GameHistories.FindAsync(id);
        }

        public async Task<GameHistory> CreateGameHistoryTicket(GameHistory ticket)
        {

            _databaseContext.GameHistories.Add(ticket);
            await _databaseContext.SaveChangesAsync();

            return ticket;
        }

    }
}
