using Backend.DTO.JackpotDTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces.IJackpot
{
    public interface IJackpotRepository
    {
        Task<decimal> GetCurrentJackpotByGameIdAsync(int gameId);
        Task AddToJackpotAsync(int gameId, decimal amount);
        Task<decimal> JackpotWinAsync(int gameId);

    }
}
