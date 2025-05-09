using Backend.Games.Dice;
using Backend.Helper;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Games.Yatzy.Service
{
    public class YatzyGameService : IYatzyGameService
    {


        private static Random _random = new Random();
        private const double HouseEdge = 0.02; // 2% house edge

        #region gameVaribles & payouts
        // Game Variables
        private const int minDiceValue = 1;
        private const int maxDiceValue = 6;
        private const int numbOfDice = 5;

        // Payouts Odds
        private const double zeroMatch = 0.00;
        private const double twoMatches = 0.10;
        private const double twoAndTwoMatches = 2.00;
        private const double threeMatches = 3.00;
        private const double threeAndTwoMatches = 4.00;
        private const double fourMatches = 5.00;
        private const double fiveMatches = 50.00;
        #endregion


        public YatzyGameResult PlayGame(YatzyGameRequest request)
        {
            List<int> numbers = new List<int>();

            for (int i = 0; i < numbOfDice; i++)
            {
                int _tempNum = _random.Next(minDiceValue, maxDiceValue + 1);
                numbers.Add(_tempNum);
            }


            YatzyGameResult result = CheckForWin(numbers);

            // Indsæt udbetaling af gevinst :TODO Indsæt balance controller
            double payout = request.BetAmount * result.Multiplier;


            return new YatzyGameResult
            {
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
                return new YatzyGameResult { Combination = "YATZY!! - Du har 5 ens!", Multiplier = fiveMatches };

            // 4 ens til spilleren
            if (counts.Contains(4))
                return new YatzyGameResult { Combination = "Tillykke, du har 4 ens!", Multiplier = fourMatches };

            // 3 + 2 ens til spilleren
            if (counts.SequenceEqual(new List<int> { 3, 2 }))
                return new YatzyGameResult { Combination = "Tillykke, du har fuld hus!!", Multiplier = threeAndTwoMatches };

            // 3 ens til spilleren
            if (counts.Contains(3))
                return new YatzyGameResult { Combination = "Tillykke, du har 3 ens!", Multiplier = threeMatches };

            // 2 + 2 ens til spilleren
            if (counts.Count(c => c == 2) == 2)
                return new YatzyGameResult { Combination = "Tillykke, du har 2 par", Multiplier = twoAndTwoMatches };

            // 2 ens til spilleren
            if (counts.Count(c => c == 2) == 1)
                return new YatzyGameResult { Combination = "Tillykke, du har 1 par", Multiplier = twoMatches };

            // 0 ens til spilleren
            return new YatzyGameResult { Combination = "Desværre, du har ingen kombination", Multiplier = zeroMatch };


        }

    }
}
