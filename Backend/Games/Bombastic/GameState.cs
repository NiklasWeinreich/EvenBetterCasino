namespace Backend.Games.Bombastic
{
    public class GameState
    {

        public int UserId { get; set; }
        public int GameId { get; set; }

        public int BombHealth { get; set; }
        public int CurrentClickNumber { get; set; }
        public decimal BetAmount { get; set; }
        public decimal CurrentWinAmount { get; set; }
        public decimal CurrentMulitplier { get; set; }
        public bool IsExploded { get; set; }
    }
}
