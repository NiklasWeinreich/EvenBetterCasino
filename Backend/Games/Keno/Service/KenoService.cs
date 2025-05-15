
using Azure.Core;
using System;
using System.Security.Cryptography;

namespace Backend.Games.Keno.Service
{
    public class KenoService : IKenoService
    {

        private const int totalNumber = 40;
        private const int numbersDrawn = 10;

        public List<KenoOdds> GetOdds(List<int> playerNumbers)
        {

            if (playerNumbers.Count < 1 || playerNumbers.Count > 10)
            {
                throw new ArgumentException("Du kan maks vælge 10 numre.");
            }

            var payoutTable = PayoutTable.FixedPayoutTables[playerNumbers.Count];

            var oddsTable = new List<KenoOdds>();
            for (int i = 0; i <= playerNumbers.Count; i++)
            {
                double multiplier = payoutTable.ContainsKey(i) ? payoutTable[i] : 0.0;
                oddsTable.Add(new KenoOdds { PlayerNumbers = i, Multiplier = multiplier });
            }
            return oddsTable;
            
        }

        public List<int> GetRandomPlayerNumbers(int amountOfNumbers)
        {

            if (amountOfNumbers is > 10 or < 1) 
            { 
                throw new ArgumentException("Forkert input. Du kan kun vælge mellem 1 - 10 numre."); 
            }


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

        public (List<int> DrawnNumbers, int Matches, double Multiplier) PlayGame(List<int> playerNumbers)
        {

            var availableNumber = MakeAvailableNumberList(totalNumber);
            var drawnNumbers = new List<int>();
            int matches = 0;


            for (int i = 0; i < numbersDrawn; i++)
            {
                int index = RandomNumberGenerator.GetInt32(0, availableNumber.Count);
                drawnNumbers.Add(availableNumber[index]);
                availableNumber.RemoveAt(index);
            }

            drawnNumbers.Sort();

            matches = drawnNumbers.Intersect(playerNumbers).Count();

            double multiplier = GetPayoutMultiplier(playerNumbers.Count, matches);


            return (drawnNumbers, matches, multiplier);
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

        double GetPayoutMultiplier(int playerCount, int hits)
        {
            if (PayoutTable.FixedPayoutTables.TryGetValue(playerCount, out var payoutTable))
            {
                if (payoutTable.TryGetValue(hits, out var multiplier))
                {
                    return multiplier;
                }
            }
            return 0.0; // Standard hvis ingen payout defineret
        }
    }
}
