using Microsoft.AspNetCore.Mvc;

namespace Backend.Games.Yatzy
{
    public interface IYatzyGameService
    {
        Task<YatzyGameResult> PlayGame(int userId, decimal betAmount);
    }
}
