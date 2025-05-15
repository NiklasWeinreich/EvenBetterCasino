namespace Backend.Games.Yatzy
{
    public class YatzyGameResult
    {

        public List<int> DiceRolls { get; set; }
        public string Combination { get; set; }
        public decimal Multiplier { get; set; }
        public decimal Payout { get; set; }
        public bool IsWin { get; set; }


    }
}
