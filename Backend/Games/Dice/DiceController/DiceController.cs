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
        private readonly IBalanceService _balanceService;

        public DiceController(IDiceGameService diceGameService, IBalanceService balanceService)
        {
            _diceGameService = diceGameService;
            _balanceService = balanceService;
            

        }


        [HttpPost("playGame")]
        public async Task<IActionResult> PlayGame([FromBody] DiceClickRequest request)
        {


            if (request.PlayerNumber < 1 || request.PlayerNumber > 100)
                return BadRequest("Player number must be between 1 and 100.");


            var balance = await _balanceService.PlaceBetAsync(request.UserId, request.BetAmount);
            if (balance < 0) // Hvis der er et problem med trækningen af penge (f.eks. utilsigtet negativ balance)
                return BadRequest("Fejl - Kunne ikke trække spilløb fra saldo.");


            var result = await _diceGameService.PlayGame(request.PlayerNumber, request.IsGuessOver, request.BetAmount);

            if (result.IsWin)
            {
                await _balanceService.WinAmountAsync(request.UserId, result.Payout);
            }


            return Ok(result);

        }

    }
}
