namespace Backend.Games.Bombastic
{
    public class BombasticStartGameRequest
    {

        public int UserId { get; set; }

        public int GameId { get; set; }

        public decimal BetAmount { get; set; }

    }
}
