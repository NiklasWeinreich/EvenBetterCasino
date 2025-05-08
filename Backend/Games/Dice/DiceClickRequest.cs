namespace Backend.Games.Dice
{
    public class DiceClickRequest
    {
        public int PlayerNumber { get; set; }
        public bool IsGuessOver { get; set; }
        public double BetAmount { get; set; }
    }
}
