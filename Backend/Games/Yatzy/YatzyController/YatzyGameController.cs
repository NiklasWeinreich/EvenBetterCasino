using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Games.Yatzy;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq;
using Azure.Core;
using Backend.Interfaces.IBalance;

namespace Backend.Games.Yatzy.YatzyController
{
    [ApiController]
    [Route("api/[controller]")]
    public class YatzyGameController : ControllerBase
    {

        private readonly IYatzyGameService _yatzyGameService;
        private readonly IBalanceService _balanceService;

        public YatzyGameController(IYatzyGameService yatzyGameService, IBalanceService balanceService)
        {
            _yatzyGameService = yatzyGameService;
            _balanceService = balanceService;
        }


        [HttpPost("playGame")]
        public async Task<IActionResult> PlayGame([FromBody] YatzyGameRequest request)
        {

            var balance = await _balanceService.PlaceBetAsync(request.UserId, request.BetAmount);
            if (balance < 0) // Hvis der er et problem med trækningen af penge (f.eks. utilsigtet negativ balance)
                return BadRequest("Fejl - Kunne ikke trække spilløb fra saldo.");

            var result = await _yatzyGameService.PlayGame(request.UserId, request.BetAmount);

            if (result.IsWin)
            {
                await _balanceService.WinAmountAsync(request.UserId, result.Payout);
            }

            return Ok(result);
        }
    }
}
