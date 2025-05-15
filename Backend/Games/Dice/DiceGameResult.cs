namespace Backend.Games.Dice
{
    public class DiceGameResult
    {
        public bool IsWin { get; set; }
        public int DiceValue { get; set; }
        public decimal WinProbability { get; set; }
        public decimal Payout { get; set; }
    }
}
