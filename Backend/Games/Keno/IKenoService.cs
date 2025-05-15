namespace Backend.Games.Keno
{
    public interface IKenoService
    {
        Task<List<KenoOdds>> GetOdds(List<int> playerNumbers);
        Task<List<int>> GetRandomPlayerNumbers(int amountOfNumbers);
        Task<KenoGameResult> PlayGame(int userId, List<int> playerNumbers, decimal betAmount);

    }
}
