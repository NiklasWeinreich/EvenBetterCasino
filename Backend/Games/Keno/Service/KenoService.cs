
using Backend.Interfaces.IBalance;
using System;
using System.Security.Cryptography;

namespace Backend.Games.Keno.Service
{
    public class KenoService : IKenoService
    {


        private const int totalNumber = 40;
        private const int numbersDrawn = 10;

        private readonly IBalanceService _balanceService;

        public KenoService(IBalanceService balanceService)
        {
            _balanceService = balanceService;
        }

        public async Task<List<KenoOdds>> GetOdds(List<int> playerNumbers)
        {

            if (playerNumbers.Count < 1 || playerNumbers.Count > 10)
            {
                throw new ArgumentException("Du kan maks vælge 10 numre.");
            }

            var payoutTable = PayoutTable.FixedPayoutTables[playerNumbers.Count];

            var oddsTable = new List<KenoOdds>();
            for (int i = 0; i <= playerNumbers.Count; i++)
            {
                decimal multiplier = payoutTable.ContainsKey(i) ? payoutTable[i] : 0.0m;
                oddsTable.Add(new KenoOdds { PlayerNumbers = i, Multiplier = multiplier });
            }
            return oddsTable;
            
        }

        public async Task<List<int>> GetRandomPlayerNumbers(int amountOfNumbers)
        {

            
            var availableNumber = MakeAvailableNumberList(totalNumber);
            var selectedNumbers = new List<int>();


            for (int i = 0; i < amountOfNumbers; i++)
            {
                int tempNum = RandomNumberGenerator.GetInt32(0, availableNumber.Count);
                selectedNumbers.Add(availableNumber[tempNum]);
                availableNumber.RemoveAt(tempNum);
            }

            selectedNumbers.Sort();

            return selectedNumbers;

        }

        public async Task<KenoGameResult> PlayGame(int userId, List<int> playerNumbers, decimal betAmount)
        {

            var balance = await _balanceService.PlaceBetAsync(userId, betAmount);
            if (balance < 0)
                throw new InvalidOperationException("Fejl - Kunne ikke trække spilbeløb fra saldo.");


            var availableNumber = MakeAvailableNumberList(totalNumber);
            var drawnNumbers = new List<int>();


            for (int i = 0; i < numbersDrawn; i++)
            {
                int index = RandomNumberGenerator.GetInt32(0, availableNumber.Count);
                drawnNumbers.Add(availableNumber[index]);
                availableNumber.RemoveAt(index);
            }

            drawnNumbers.Sort();

            int matches = drawnNumbers.Intersect(playerNumbers).Count();


            decimal multiplier = GetPayoutMultiplier(playerNumbers.Count, matches);
            
            bool isWin = multiplier > 0;

            decimal payout = isWin ? Math.Round( betAmount * multiplier)
                : 0;

            if (payout > 0)
            {
                await _balanceService.WinAmountAsync(userId, payout);
            }



            return new KenoGameResult
            {
                DrawnNumbers = drawnNumbers,
                Matches = matches,
                Multiplier = multiplier,
                IsWin = isWin,
                Payout = payout
            
            };
        }

        private List<int> MakeAvailableNumberList(int maxNumber)
        {

            var tempList = new List<int>();

            for (int i = 0; i < maxNumber; i++)
            {
                tempList.Add(i + 1);
            }

            return tempList;
        }

        private decimal GetPayoutMultiplier(int playerCount, int hits)
        {
            if (PayoutTable.FixedPayoutTables.TryGetValue(playerCount, out var payoutTable))
            {
                if (payoutTable.TryGetValue(hits, out decimal multiplier))
                {
                    
                    return multiplier;
                }
            }
            return 0.0m; // Standard hvis ingen payout defineret
        }
    }
}
