namespace Backend.Games.Dice
{
    public class DiceGameResult
    {
        public bool IsWin { get; set; }
        public int DiceValue { get; set; }
        public double WinProbability { get; set; }
        public double Payout { get; set; }
    }
}
