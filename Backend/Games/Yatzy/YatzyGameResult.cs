namespace Backend.Games.Yatzy
{
    public class YatzyGameResult
    {
        public List<int> DiceRolls { get; set; }
        public string Combination { get; set; }
        public double Multiplier { get; set; }
        public double Payout { get; set; }

    }
}
