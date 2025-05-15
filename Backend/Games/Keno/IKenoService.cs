namespace Backend.Games.Keno
{
    public interface IKenoService
    {
        List<KenoOdds> GetOdds(List<int> playerNumbers);
        List<int> GetRandomPlayerNumbers(int amountOfNumbers);
        (List<int> DrawnNumbers, int Matches, double Multiplier) PlayGame(List<int> playerNumbers);

    }
}
