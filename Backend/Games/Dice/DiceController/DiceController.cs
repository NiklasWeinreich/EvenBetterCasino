using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Games.Dice;
using System.Collections.Concurrent;
using Backend.Games.Bombastic;
using Backend.Interfaces.IBalance;


namespace Backend.Games.Dice.DiceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {

        private readonly IDiceGameService _diceGameService;

        public DiceController(IDiceGameService diceGameService, IBalanceService balanceService)
        {
            _diceGameService = diceGameService;
        }


        [HttpPost("playGame")]
        public async Task<IActionResult> PlayGame([FromBody] DiceGameRequest request)
        {


            if (request.PlayerNumber < 2 || request.PlayerNumber > 99)
                return BadRequest("Player number must be between 2 and 99.");


            var result = await _diceGameService.PlayGame(request.UserId, request.GameId, request.PlayerNumber, request.IsGuessOver, request.BetAmount);

            return Ok(result);

        }

    }
}
