using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Games.Bombastic;


namespace Backend.Games.Bombastic.BombasticController
{
    [ApiController]
    [Route("api/[controller]")]
    public class BombasticGameController : ControllerBase
    {

        private static Random _random = new Random();
        private static ConcurrentDictionary<string, GameState> currentGamesDict = new ConcurrentDictionary<string, GameState>();
        private const int MaxBombHealth = 20; // Maksimum antal klik
        private const double HouseEdge = 0.02; // 2% house edge



        [HttpPost("startGame")]
        public IActionResult startGame([FromBody] BombasticStartGameRequest request)
        {

            // TO DO: Indsæt funktion, der tjekker saldo for spilleren.


            // Generer en unik spiller-ID (kan være en GUID eller et tilfældigt tal)
            string sessionId = Guid.NewGuid().ToString();

            int currentGameBombHealth = _random.Next(1, MaxBombHealth + 1);


            // Opretter et nyt spil for den speciffikke spiller
            var gameState = new GameState
            {
                BetAmount = request.BetAmount,
                BombHealth = currentGameBombHealth,
                CurrentClickNumber = 0,
                CurrentWinAmount = 0,
                CurrentMulitplier = 0,
                IsExploded = false
            };

            // Tilknyt spiloplysninger med sessionId
            currentGamesDict[sessionId] = gameState;


            return Ok(new { message = "Spillet er startet! Tryk på bomben, men pas på! - Den må ikke sprænge, for så mister du din potentielle gevinst.", sessionId });


        }

        [HttpPost("clickBomb")]
        public IActionResult ClickBomb([FromBody] BombasticClickRequest request)
        {

            // Hent spillerens GameState ved hjælp af playerId
            if (!currentGamesDict.TryGetValue(request.SessionId, out GameState gameState))
            {
                return BadRequest(new { message = "Ingen session med det speciffikke ID er igangværende eller startet" });
            }

            if (gameState.CurrentClickNumber + 1 == gameState.BombHealth)
            {
                currentGamesDict.TryRemove(request.SessionId, out gameState);
                return Ok(new
                {
                    isExploded = true,
                    currentClickNumber = gameState.CurrentClickNumber,
                    currentWinAmount = 0,
                    CurrentMulitplier = 1,
                    message = "Bomben sprang, og du døde."
                });
            }
            else if (gameState.CurrentClickNumber + 1 == MaxBombHealth)
            {
                currentGamesDict.TryRemove(request.SessionId, out gameState);
                return Ok(new
                {
                    isExploded = false,
                    currentClickNumber = 20,
                    currentWinAmount = gameState.CurrentWinAmount,
                    currentMulitplier = gameState.CurrentMulitplier,
                    message = "Tillykke du desarmeret bomben."
                });

            }
            else
            {
                gameState.CurrentClickNumber++;
                gameState.CurrentMulitplier++;

                Console.WriteLine($"BetAmount: {gameState.BetAmount}, Multiplier: {gameState.CurrentMulitplier}, WinAmount: {gameState.CurrentWinAmount}");
                gameState.CurrentWinAmount = gameState.BetAmount * gameState.CurrentMulitplier;

                return Ok(new
                {
                    isExploded = false,
                    currentClickNumber = gameState.CurrentClickNumber,
                    currentWinAmount = gameState.CurrentWinAmount,
                    currentMulitplier = gameState.CurrentMulitplier,
                    message = "Du er safe! - Bliver du ved eller casher du ud?"
                });

            }

        }
    }
}
