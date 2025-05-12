using Backend.Database.DatabaseContext;
using Backend.DTO.JackpotDTO;
using Backend.Interfaces.IBalance;
using Backend.Interfaces.IJackpot;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Repositories.JackpotRepository
{
    public class JackpotRepository : IJackpotRepository
    {
        private readonly DatabaseContext _context;
        private readonly IBalanceService _balanceService;

        public JackpotRepository(DatabaseContext dbContext, IBalanceService balanceService) 
        {
            _context = dbContext;
            _balanceService = balanceService;

        }

        public async Task<decimal> GetCurrentJackpotByGameIdAsync(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null) throw new Exception("Game not found");

            return game.JackpotAmount;
        }

        public async Task AddToJackpotAsync(int gameId, decimal amount)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null) throw new Exception("Game not found");

            game.JackpotAmount += amount;
            await _context.SaveChangesAsync();
            
        }

        public async Task<decimal> JackpotWinAsync(int gameId)
        {
            var game = await _context.Games.FindAsync(gameId);
            if (game == null) throw new Exception("Game not found");

            var winAmount = game.JackpotAmount;
            if (winAmount <= 0) throw new Exception("No jackpot available");

            game.JackpotAmount = 10000;

            await _context.SaveChangesAsync();

            return winAmount;
        }
    }
}
