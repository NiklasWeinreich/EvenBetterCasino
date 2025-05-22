namespace Backend.Games.Bombastic
{
    public class BombasticGameResult
    {
        public int CurrentClickNumber { get; set; }
        public decimal CurrentWinAmount { get; set; }
        public decimal CurrentMulitplier { get; set; }
        public bool IsExploded { get; set; }
        public string Message { get; set; }

    }
}
