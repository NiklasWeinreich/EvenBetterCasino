namespace Backend.Games.Dice
{
    public class DiceGameRequest
    {
        public int UserId { get; set; }
        public int GameId { get; set; }
        public int PlayerNumber { get; set; }
        public bool IsGuessOver { get; set; }
        public decimal BetAmount { get; set; }
    }
}
