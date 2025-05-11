namespace Backend.Games.Dice
{
    public interface IDiceGameService
    {
        Task<DiceGameResult> PlayGame(int playerNumber, bool isGuessOver, decimal betAmount);

    }
}
