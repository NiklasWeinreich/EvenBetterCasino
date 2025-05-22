using Microsoft.AspNetCore.Mvc;

namespace Backend.Games.Bombastic
{
    public interface IBombasticService
    {

        Task<string> StartGame([FromBody] BombasticStartGameRequest request);
        Task<BombasticGameResult> PlayGame([FromBody] BombasticClickRequest request);

    }
}
