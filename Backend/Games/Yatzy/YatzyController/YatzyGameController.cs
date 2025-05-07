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
        public IActionResult PlayGame([FromBody] YatzyGameRequest request)
        {

            var result = _yatzyGameService.PlayGame(request);

            return Ok(result);
        }
    }
}
