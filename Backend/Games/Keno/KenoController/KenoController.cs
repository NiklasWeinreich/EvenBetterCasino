using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Backend.Games.Keno;
using System.Numerics;
using Azure.Core;
using Backend.Games.Keno.Service;

namespace Backend.Games.Keno.KenoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KenoController : ControllerBase
    {

        private int totalNumber = 40;
        private readonly IKenoService _kenoService;

        public KenoController(IKenoService kenoService)
        {
            _kenoService = kenoService;
        }

        [HttpPost("getOdds")]
        public IActionResult GetOdds([FromBody] KenoStartGameRequest request)
        {

            var validationResult = CheckForValidInput(request.PlayerNumbers);
            if (validationResult != null) return validationResult;


            var odds = _kenoService.GetOdds(request.PlayerNumbers);
            
            return Ok(odds);


        }


        [HttpPost("getRandomPlayerNumbers")]
        public IActionResult GetRandomPlayerNumbers(int amountOfNumbers)
        {

            try
            {
                
                var numbers = _kenoService.GetRandomPlayerNumbers(amountOfNumbers);
                
                return Ok(new { message = "Your numbers:", playerNumberList = numbers });
            
            }
            catch (ArgumentException ex)
            {
            
                return BadRequest(new { message = ex.Message });
            
            }


        }


        [HttpPost("playGame")]
        public IActionResult PlayGame([FromBody] KenoStartGameRequest request)
        {

            var validationResult = CheckForValidInput(request.PlayerNumbers);
            if (validationResult != null) return validationResult;


            var result = _kenoService.PlayGame(request.PlayerNumbers);
            return Ok(new
            {
                message = "Yeeeehaaaay! - Det virker!",
                drawnedNumbers = result.DrawnNumbers,
                currectMatches = result.Matches,
                multiplier = result.Multiplier
            });

        }

        private IActionResult CheckForValidInput(List<int> playerNumbers)
        {
            if (playerNumbers is null || playerNumbers.Count is < 1 or > 10)
            {
                return BadRequest(new { message = "Du skal vælge mellem 1 og 10 tal." });
            }

            if (playerNumbers.Any(n => n < 1 || n > totalNumber))
            {
                return BadRequest(new { message = $"Du må kun vælge tal mellem 1 og {totalNumber}." });
            }

            if (playerNumbers.Distinct().Count() != playerNumbers.Count)
            {
                return BadRequest(new { message = "Du må ikke vælge det samme tal flere gange." });
            }

            return null;
        }

    }

}
