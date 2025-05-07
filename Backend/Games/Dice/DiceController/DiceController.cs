using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Backend.Games.Dice;
using System.Collections.Concurrent;
using Backend.Games.Bombastic;


namespace Backend.Games.Dice.DiceController
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiceController : ControllerBase
    {

        private readonly IDiceGameService _diceGameService;

        public DiceController(IDiceGameService diceGameService)
        {
            _diceGameService = diceGameService;

        }


        [HttpPost("playGame")]
        public IActionResult PlayGame([FromBody] DiceClickRequest request)
        {


            if (request.PlayerNumber < 1 || request.PlayerNumber > 100)
                return BadRequest("Player number must be between 1 and 100.");

            if (request.BetAmount <= 0)
                return BadRequest("Bet amount must be greater than zero.");

            var result = _diceGameService.PlayGame(request.PlayerNumber, request.IsGuessOver, request.BetAmount);

            //if (result.IsWin)
            //{
            //    await _balanceService.DepositAsync(request.UserId, result.Payout);
            //}


            return Ok(result);

        }

    }
}
