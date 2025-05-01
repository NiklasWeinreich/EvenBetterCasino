using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Backend.Games.Keno;
using System.Numerics;
using Azure.Core;

namespace Backend.Games.Keno.KenoController
{
    [Route("api/[controller]")]
    [ApiController]
    public class KenoController : ControllerBase
    {

        static Random _random = new Random();
        static ConcurrentDictionary<string, GameState> currentGamesDict = new ConcurrentDictionary<string, GameState>();
        const int numbersDrawn = 10; // Antallet af tal der bliver udtrukket
        const int totalNumber = 40; // Antallet af tal
        int currectMatches = 0;
        List<int> availableNumberList = new List<int>();
        List<int> playerNumberList = new List<int>();
        List<int> drawnedNumbers = new List<int>();
        PayoutTable payoutTable = new PayoutTable();



        [HttpPost("getOdds")]
        public IActionResult GetOdds([FromBody] KenoStartGameRequest request)
        {
            /// TO DO: Spilleren skal ikke kunne vælge den samme.

            if (request.PlayerNumbers.Count is < 1 or > 10)
            {
                return BadRequest(new { message = "Du kan maks vælge 10 numre." });
            }

            var payoutTable = PayoutTable.FixedPayoutTables[request.PlayerNumbers.Count];

            var multiplierList = new List<KenoOdds>();
            for (int playerNumbers = 0; playerNumbers <= request.PlayerNumbers.Count; playerNumbers++)
            {

                double multiplier = payoutTable.ContainsKey(playerNumbers) ? payoutTable[playerNumbers] : 0.0;

                multiplierList.Add(new KenoOdds
                {
                    PlayerNumbers = playerNumbers,
                    Multiplier = multiplier
                });
            }

            return Ok(multiplierList);
        }

        [HttpPost("getRandomNumbers")]
        public IActionResult PlayGame(int amountOfNumbers)
        {

            if (amountOfNumbers is > 10 or < 1){ return BadRequest("Wrong input. The range has to be between 1 - 10."); }
 
            availableNumberList = MakeAvailableNumberList(totalNumber);


            for (int i = 0; i < amountOfNumbers; i++)
            {
                int tempNum = _random.Next(0, availableNumberList.Count);
                playerNumberList.Add(availableNumberList[tempNum]);
                availableNumberList.RemoveAt(tempNum);
            }

            playerNumberList.Sort();

            return Ok(new { message = "Your numbers is:", playerNumberList });



        }


        [HttpPost("drawNumbers")]
        public IActionResult DrawNumbers([FromBody] KenoStartGameRequest request)
        {

            availableNumberList = MakeAvailableNumberList(totalNumber);
            
            for (int i = 0; i < numbersDrawn; i++)
            {
                int drawnNum = _random.Next(0, availableNumberList.Count);
                drawnedNumbers.Add(availableNumberList[drawnNum]);
                availableNumberList.RemoveAt(drawnNum);

            }

            request.PlayerNumbers.Sort();
            drawnedNumbers.Sort();

            for (int i = 0; i < drawnedNumbers.Count; i++)
            {

                if (request.PlayerNumbers.Contains(drawnedNumbers[i])) 
                {
                    currectMatches++; 
                }

            }


            double multiplier = GetPayoutMultiplier(request.PlayerNumbers.Count, currectMatches);


            return Ok(new { message = "Yeeeehaaaay! - Det virker!", drawnedNumbers, currectMatches, multiplier});

        }


        double GetPayoutMultiplier(int playerCount, int hits)
        {
            if (PayoutTable.FixedPayoutTables.TryGetValue(playerCount, out var payoutTable))
            {
                if (payoutTable.TryGetValue(hits, out var multiplier))
                {
                    return multiplier;
                }
            }
            return 0.0; // Standard hvis ingen payout defineret
        }



        private List<int> MakeAvailableNumberList(int maxNumber)
        {
            for (int i = 0; i < maxNumber; i++)
            {
                availableNumberList.Add(i + 1);
            }

            return availableNumberList;
        }


        // Skal denne rykkes ud??? 
        public class KenoOdds
        {
            public int PlayerNumbers { get; set; }
            public double Multiplier { get; set; }

        }

    }

        //[HttpPost("startGame")]
        //public IActionResult StartGame([FromBody] KenoStartGameRequest request)
        //{

        //    // Udtræk numre fra 1-40
        //        // Samme nummer kan IKKE udtrækkes.




        //    // udtræk numre spilleren kan vælge (1-40)
        //    for (int i = 0; i < maxNumber; i++)
        //    {
        //        _random.Next(1, maxNumber + 1);
        //    }


        //    // Hvor mange numre fra 1-10 har spilleren valgt at vil satse på?
        //    // Spillerens valgte numre

        //    // Check for om spilleren har ramt numre

        //    // Udbetal spilleren

        //    return Ok();


        //}


    /// skal man kunne ændre i odds via CRUD? 

}
