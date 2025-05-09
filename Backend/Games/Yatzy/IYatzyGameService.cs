using Microsoft.AspNetCore.Mvc;

namespace Backend.Games.Yatzy
{
    public interface IYatzyGameService
    {
        YatzyGameResult PlayGame(YatzyGameRequest request);
    }
}
