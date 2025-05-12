using Backend.Database.Entities;
using Backend.DTO.JackpotDTO;
using Backend.Interfaces.IBalance;
using Backend.Interfaces.IJackpot;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Services.JackpotService
{
    public class JackpotService : IJackpotService
    {

        private readonly IJackpotRepository _jackpotRepository;
        private readonly IBalanceService _balanceService;

        public JackpotService(IJackpotRepository jackpotRepository, IBalanceService balanceService)
        {
            _jackpotRepository = jackpotRepository;
            _balanceService = balanceService;
        }

        public async Task<decimal> GetCurrentJackpotByGameIdAsync(int gameId)
        {
            var result = await _jackpotRepository.GetCurrentJackpotByGameIdAsync(gameId);

            return result;
        }

        public async Task<JackpotSetResponse> DepositToJackpotAsync(int gameId, decimal amount)
        {
            await _jackpotRepository.AddToJackpotAsync(gameId, amount);

            return new JackpotSetResponse
            {
                GameId = gameId,
                Amount = amount,
                Message = "Jackpot amount updated"
            };
        }

        public async Task<JackpotWinResponse> JackpotWinAsyc(int gameId, int userId)
        {

            var winAmount = await _jackpotRepository.JackpotWinAsync(gameId);

            var player = await _balanceService.WinAmountAsync(userId, winAmount);

            return new JackpotWinResponse
            {
                GameId = gameId,
                AmountWon = winAmount,
                Message = "Jackpot claimed successfully"

            };
        }
    }
}
