namespace Backend.Games.Dice
{
    public interface IDiceGameService
    {
        DiceGameResult PlayGame(int playerNumber, bool isGuessOver, double betAmount);

    }
}
