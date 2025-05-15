namespace Backend.Games.Keno
{
    public class KenoGameResult
    {

        public List<int> DrawnNumbers { get; set; }
        public int Matches { get; set; }
        public decimal Multiplier { get; set; }
        public bool IsWin {  get; set; } 
        public decimal Payout { get; set; }


    }
}
