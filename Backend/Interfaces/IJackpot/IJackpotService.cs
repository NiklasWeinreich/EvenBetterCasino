using Backend.DTO.JackpotDTO;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Interfaces.IJackpot
{
    public interface IJackpotService
    {
        Task<decimal> GetCurrentJackpotByGameIdAsync(int gameId);

        Task<JackpotSetResponse> DepositToJackpotAsync(int gameId, decimal amount);
        Task<JackpotWinResponse> JackpotWinAsyc(int gameId, int userId);


    }
}
