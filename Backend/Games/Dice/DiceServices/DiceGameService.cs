using System.Security.Cryptography;

namespace Backend.Games.Dice.DiceServices
{
    public class DiceGameService : IDiceGameService
    {

        private const double _houseEdge = 0.02; // 2%
        private const int minDiceValue = 1; // Maksimum antal klik
        private const int maxDiceValue = 100; // Maksimum antal klik


        public DiceGameResult PlayGame(int playerNumber, bool isGuessOver, double betAmount)
        {

            int diceValue = RandomNumberGenerator.GetInt32(minDiceValue, maxDiceValue + 1);

            bool isWin = isGuessOver 
                ? playerNumber <= diceValue 
                : playerNumber >= diceValue;

            double winProbability = isGuessOver
                ? playerNumber == 100 ? 0.01 : (maxDiceValue - playerNumber + 1) / 100.0
                : playerNumber == 1 ? 0.01 : (playerNumber - minDiceValue + 1) / 100.0;

            double payout = isWin
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
