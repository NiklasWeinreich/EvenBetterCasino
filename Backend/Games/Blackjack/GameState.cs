namespace Backend.Games.Blackjack
{
    public class GameState
    {

        public int CurrentClickNumber { get; set; }
        public double BetAmount { get; set; }
        public double CurrentWinAmount { get; set; }
        public double CurrentMulitplier { get; set; }
        public bool IsExploded { get; set; }
    }
}
