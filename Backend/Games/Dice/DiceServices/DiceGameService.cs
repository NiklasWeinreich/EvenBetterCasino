using Backend.Interfaces.IBalance;
using System.Security.Cryptography;

namespace Backend.Games.Dice.DiceServices
{
    public class DiceGameService : IDiceGameService
    {

        private const decimal _houseEdge = 0.02m; // 2%
        private const int minDiceValue = 1; // Maksimum antal klik
        private const int maxDiceValue = 100; // Maksimum antal klik

        private readonly IBalanceService _balanceService;

        public DiceGameService(IBalanceService balanceService)
        {
            _balanceService = balanceService;

        }


        public async Task<DiceGameResult> PlayGame(int userId, int playerNumber, bool isGuessOver, decimal betAmount)
        {

            // Kald balance service.
            var balance = await _balanceService.PlaceBetAsync(userId, betAmount);
            if (balance < 0) throw new InvalidOperationException("Fejl - Kunne ikke trække spilløb fra saldo.");


            int diceValue = RandomNumberGenerator.GetInt32(minDiceValue, maxDiceValue + 1);

            bool isWin = isGuessOver 
                ? playerNumber < diceValue 
                : playerNumber > diceValue;

            decimal winProbability = isGuessOver
                ? playerNumber == 99 ? 0.01m : (maxDiceValue - playerNumber + 1) / 100m
                : playerNumber == 2 ? 0.01m : (playerNumber - minDiceValue + 1) / 100m;


            decimal payout = isWin
                ? Math.Round((betAmount / winProbability) * (1 - _houseEdge), 2)
                : 0;

            if (isWin)
            {
                await _balanceService.WinAmountAsync(userId, payout);
            }

            return new DiceGameResult
            {
                IsWin = isWin,
                DiceValue = diceValue,
                WinProbability = winProbability,
                Payout = payout
            };

        }
    }
}
