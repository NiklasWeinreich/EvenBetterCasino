namespace Backend.Games.Dice
{
    public interface IDiceGameService
    {
        Task<DiceGameResult> PlayGame(int userId, int gameId, int playerNumber, bool isGuessOver, decimal betAmount);

    }
}
