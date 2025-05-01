using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Games.Yatzy;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Linq;

namespace Backend.Games.Yatzy.YatzyController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlackjackGameController : ControllerBase
    {

        private static Random _random = new Random();
        List<int> numbers = new List<int>();
        private static ConcurrentDictionary<string, GameState> currentGamesDict = new ConcurrentDictionary<string, GameState>();
        private const double HouseEdge = 0.02; // 2% house edge

        #region gameVaribles & payouts
        // Game Variables
        private const int minDiceValue = 1;
        private const int maxDiceValue = 6;
        private const int numbOfDice = 5;

        // Payouts Odds
        private const double zeroMatch = 0.00;
        private const double twoMatches = 0.10;
        private const double twoAndTwoMatches = 2.00;
        private const double threeMatches = 3.00;
        private const double threeAndTwoMatches = 4.00;
        private const double fourMatches = 5.00;
        private const double fiveMatches = 50.00;
        #endregion



        [HttpPost("startGame")]
        public IActionResult StartGame([FromBody] YatzyStartGameRequest request)
        {

            // TO DO: Indsæt funktion, der tjekker saldo for spilleren.


            // Generer en unik spiller-ID (kan være en GUID eller et tilfældigt tal)
            // string sessionId = Guid.NewGuid().ToString();
            string sessionId = "1";


            // Opretter et nyt spil for den speciffikke spiller
            var gameState = new GameState
            {
                BetAmount = request.BetAmount,
                WinAmount = 0,
                IsWin = false
            };

            // Tilknyt spiloplysninger med sessionId
            currentGamesDict[sessionId] = gameState;

            return Ok(new { message = sessionId });


        }

        [HttpPost("throwDice")]
        public IActionResult ThrowDice([FromBody] YatzyClickRequest request)
        {

            // Hent spillerens GameState ved hjælp af playerId
            if (!currentGamesDict.TryGetValue(request.SessionId, out GameState gameState))
            {
                return BadRequest(new { message = "Ingen session med det speciffikke ID er igangværende eller startet" });
            }

            for (int i = 0; i < numbOfDice; i++)
            {
                int _tempNum = _random.Next(minDiceValue, maxDiceValue + 1);
                numbers.Add(_tempNum);
            }


            WinResult result = CheckForWin(numbers);
       
            // Indsæt udbetaling af gevinst

            return Ok(new { message = numbers, result });

        }

        private WinResult CheckForWin(List<int> numbers)
        {

            var counts = numbers
            .GroupBy(n => n)
            .Select(g => g.Count())
            .ToList();

            
            
            // 5 ens til spilleren
            if (counts.Contains(5))
                return new WinResult { Combination = "YATZY!! - Du har 5 ens!", Multiplier = fiveMatches };

            // 4 ens til spilleren
            if (counts.Contains(4))
                return new WinResult { Combination = "Tillykke, du har 4 ens!", Multiplier = fourMatches };

            // 3 + 2 ens til spilleren
            if (counts.SequenceEqual(new List<int> { 3, 2 }))
                return new WinResult { Combination = "Tillykke, du har fuld hus!!", Multiplier = threeAndTwoMatches };

            // 3 ens til spilleren
            if (counts.Contains(3))
                return new WinResult { Combination = "Tillykke, du har 3 ens!", Multiplier = threeMatches };

            // 2 + 2 ens til spilleren
            if (counts.Count(c => c == 2) == 2) 
                return new WinResult { Combination = "Tillykke, du har 2 par", Multiplier = twoAndTwoMatches };

            // 2 ens til spilleren
            if (counts.Count(c => c == 2) == 1)
                return new WinResult { Combination = "Tillykke, du har 1 par", Multiplier = twoMatches }; 

            // 0 ens til spilleren
            return new WinResult { Combination = "Desværre, du har ingen kombination", Multiplier = zeroMatch }; 


        }

    }
}
