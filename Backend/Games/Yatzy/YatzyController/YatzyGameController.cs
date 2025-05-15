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

        public YatzyGameController(IYatzyGameService yatzyGameService)
        {
            _yatzyGameService = yatzyGameService;
        }


        [HttpPost("playGame")]
        public async Task<IActionResult> PlayGame([FromBody] YatzyGameRequest request)
        {


            var result = await _yatzyGameService.PlayGame(request.UserId, request.GameId, request.BetAmount);


            return Ok(result);
        }
    }
}
