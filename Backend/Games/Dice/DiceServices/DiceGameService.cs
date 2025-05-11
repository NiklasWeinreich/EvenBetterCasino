using System.Security.Cryptography;

namespace Backend.Games.Dice.DiceServices
{
    public class DiceGameService : IDiceGameService
    {

        private const decimal _houseEdge = 0.02m; // 2%
        private const int minDiceValue = 1; // Maksimum antal klik
        private const int maxDiceValue = 100; // Maksimum antal klik


        public async Task<DiceGameResult> PlayGame(int playerNumber, bool isGuessOver, decimal betAmount)
        {

            int diceValue = RandomNumberGenerator.GetInt32(minDiceValue, maxDiceValue + 1);

            bool isWin = isGuessOver 
                ? playerNumber <= diceValue 
                : playerNumber >= diceValue;

            decimal winProbability = isGuessOver
                ? playerNumber == 100 ? 0.01m : (maxDiceValue - playerNumber + 1) / 100m
                : playerNumber == 1 ? 0.01m : (playerNumber - minDiceValue + 1) / 100m;


            decimal payout = isWin
                ? Math.Round((betAmount / winProbability) * (1 - _houseEdge), 2)
                : 0;

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
