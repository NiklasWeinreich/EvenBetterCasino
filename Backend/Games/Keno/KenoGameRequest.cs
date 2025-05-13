namespace Backend.Games.Keno
{
    public class KenoGameRequest
    {

        public int UserId { get; set; }
        public decimal BetAmount { get; set; }
        public List<int> PlayerNumbers { get; set; }

    }
}
