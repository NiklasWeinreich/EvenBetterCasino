using Backend.DTO.JackpotDTO;
using Backend.Interfaces.IJackpot;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.JackpotController
{
    [Route("api/[controller]")]
    [ApiController]
    public class JackpotController : ControllerBase
    {

        private readonly IJackpotService _jackpotService;

        public JackpotController(IJackpotService jackpotService)
        {
            _jackpotService = jackpotService;
        }

        [HttpGet("getCurrenctJackpot/{gameId}")]
        public async Task<IActionResult> GetCurrenctJackpotByGameId(int gameId)
        {
            var amount = await _jackpotService.GetCurrentJackpotByGameIdAsync(gameId);
            return Ok(amount);
        }

        [HttpPost("depositToJackpot")]
        public async Task<IActionResult> DepositToJackpot([FromBody] JackpotSetRequest request)
        {
            var result = await _jackpotService.DepositToJackpotAsync(request.GameId, request.Amount);
            return Ok(result);
        }

        [HttpPost("jackpotWin")]
        public async Task<IActionResult> JackpotWinAsync([FromBody] JackpotWinRequest request)
        {
            var result = await _jackpotService.JackpotWinAsyc(request.GameId, request.PlayerId);
            return Ok(result);
        }

    }
}
