using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Games.Bombastic;


namespace Backend.Games.Bombastic.BombasticController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BombasticGameController : ControllerBase
    {

        private readonly IBombasticService _bombasticService;

        public BombasticGameController(IBombasticService bombasticService)
        {
            _bombasticService = bombasticService;
        }


        [HttpPost("startGame")]
        public async Task<IActionResult> StartGame([FromBody] BombasticStartGameRequest request)
        {

            
            var sessionId = await _bombasticService.StartGame(request);

            return Ok(new
            {
                sessionId = sessionId,
                message = "Spillet er startet"
            });


        }

        [HttpPost("clickBomb")]
        public async Task<IActionResult> PlayGame([FromBody] BombasticClickRequest request)
        {

            var result = await _bombasticService.PlayGame(request);

            return Ok(result);

        }
    }
}
