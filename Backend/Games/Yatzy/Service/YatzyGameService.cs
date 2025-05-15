using Backend.Database.Entities;
using Backend.Games.Dice;
using Backend.Helper;
using Backend.Interfaces.IBalance;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Backend.Games.Yatzy.Service
{
    public class YatzyGameService : IYatzyGameService
    {



        #region gameVaribles & payouts
        // Game Variables
        private const int minDiceValue = 1;
        private const int maxDiceValue = 6;
        private const int numbOfDice = 5;

        // Payouts Odds
        private const decimal zeroMatch = 0.00m;
        private const decimal twoMatches = 0.10m;
        private const decimal twoAndTwoMatches = 1.00m;
        private const decimal threeMatches = 2.00m;
        private const decimal threeAndTwoMatches = 3.00m;
        private const decimal fourMatches = 5.00m;
        private const decimal fiveMatches = 50.00m;
        #endregion

        private readonly IBalanceService _balanceService;
        private readonly GameHistoryHelper _gameHistoryHelper;

        public YatzyGameService(IBalanceService balanceService, GameHistoryHelper gameHistoryHelper)
        {
            _balanceService = balanceService;
            _gameHistoryHelper = gameHistoryHelper;
        }

        public async Task<YatzyGameResult> PlayGame(int userId, int gameId, decimal betAmount)
        {

            var balance = await _balanceService.PlaceBetAsync(userId, betAmount);
            if (balance < 0)
                throw new InvalidOperationException("Fejl - Kunne ikke trække spilbeløb fra saldo.");

            List<int> numbers = new List<int>();

            for (int i = 0; i < numbOfDice; i++)
            {
                int diceValue = RandomNumberGenerator.GetInt32(minDiceValue, maxDiceValue + 1);
                numbers.Add(diceValue);
            }


            YatzyGameResult result = CheckForWin(numbers);

            decimal payout = betAmount * result.Multiplier;

            if (result.IsWin)
            {
                await _balanceService.WinAmountAsync(userId, payout);
            }

            await _gameHistoryHelper.LogGameWithoutCashOut(userId, gameId, betAmount, payout, result.IsWin);


            return new YatzyGameResult
            {
                IsWin = result.IsWin,
                DiceRolls = numbers,
                Combination = result.Combination,
                Multiplier = result.Multiplier,
                Payout = payout
            };

        }

        private YatzyGameResult CheckForWin(List<int> numbers)
        {

            var counts = numbers
            .GroupBy(n => n)
            .Select(g => g.Count())
            .ToList();


            // 5 ens til spilleren
            if (counts.Contains(5))
                return new YatzyGameResult { Combination = "Du har YATZY!!", Multiplier = fiveMatches, IsWin = true };

            // 4 ens til spilleren
            if (counts.Contains(4))
                return new YatzyGameResult { Combination = "Du har 4 ens!", Multiplier = fourMatches, IsWin = true };

            // 3 + 2 ens til spilleren
            if (counts.Contains(3) && counts.Contains(2))
                return new YatzyGameResult { Combination = "Du har fuld hus!!", Multiplier = threeAndTwoMatches, IsWin = true };

            // 3 ens til spilleren
            if (counts.Contains(3))
                return new YatzyGameResult { Combination = "Du har 3 ens!", Multiplier = threeMatches, IsWin = true };

            // 2 + 2 ens til spilleren
            if (counts.Count(c => c == 2) == 2)
                return new YatzyGameResult { Combination = "Du har 2 par", Multiplier = twoAndTwoMatches, IsWin = true };

            // 2 ens til spilleren
            if (counts.Count(c => c == 2) == 1)
                return new YatzyGameResult { Combination = "Du har 1 par", Multiplier = twoMatches, IsWin = true };

            // 0 ens til spilleren
            return new YatzyGameResult { Combination = "Desværre, du har ingen kombination", Multiplier = zeroMatch, IsWin = false};


        }

    }
}
