using Backend.Database.Entities;
using Backend.Helper;
using Backend.Interfaces.IBalance;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Concurrent;

namespace Backend.Games.Bombastic.BombasticService
{
    public class BombasticGameService : IBombasticService
    {


        private static Random _random = new Random();
        private static ConcurrentDictionary<string, GameState> currentGamesDict = new ConcurrentDictionary<string, GameState>();
        private const int MaxBombHealth = 20; // Maksimum antal klik
        private const double HouseEdge = 0.02; // 2% house edge

        private readonly IBalanceService _balanceService;
        private readonly GameHistoryHelper _gameHistoryHelper;


        public BombasticGameService(IBalanceService balanceService, GameHistoryHelper gameHistoryHelper)
        {
            _balanceService = balanceService;
            _gameHistoryHelper = gameHistoryHelper;

        }

        public async Task<string> StartGame([FromBody] BombasticStartGameRequest request)
        {

            

            // Kald balance service.
            var balance = await _balanceService.PlaceBetAsync(request.UserId, request.BetAmount);
            if (balance < 0) throw new InvalidOperationException("Fejl - Kunne ikke trække spilløb fra saldo.");

            // Generer en unik spiller-ID (kan være en GUID eller et tilfældigt tal)
            string sessionId = Guid.NewGuid().ToString();

            int currentGameBombHealth = _random.Next(1, MaxBombHealth + 1);


            // Opretter et nyt spil for den speciffikke spiller
            var gameState = new GameState
            {
                UserId = request.UserId,
                GameId = request.GameId,
                BetAmount = request.BetAmount,
                BombHealth = currentGameBombHealth,
                CurrentClickNumber = 0,
                CurrentWinAmount = 0,
                CurrentMulitplier = 0,
                IsExploded = false
            };

            // Tilknyt spiloplysninger med sessionId
            currentGamesDict[sessionId] = gameState;


            return sessionId;


        }

        public async Task<BombasticGameResult> PlayGame([FromBody] BombasticClickRequest request)
        {

            // Hent spillerens GameState ved hjælp af playerId
            if (!currentGamesDict.TryGetValue(request.SessionId, out GameState gameState))
            {
                throw new InvalidOperationException("Ingen session med det speciffikke ID er igangværende eller startet");
            }

            if (gameState.CurrentClickNumber + 1 == gameState.BombHealth)
            {

                currentGamesDict.TryRemove(request.SessionId, out gameState);
                await _gameHistoryHelper.LogGameWithoutCashOut(gameState.UserId, gameState.GameId, gameState.BetAmount, 0, false);

                return new BombasticGameResult
                {
                    IsExploded = true,
                    CurrentClickNumber = gameState.CurrentClickNumber,
                    CurrentWinAmount = 0,
                    CurrentMulitplier = 1,
                    Message = "Bomben sprang, og du døde."

            };
            }
            else if (gameState.CurrentClickNumber + 1 == MaxBombHealth)
            {
                await _gameHistoryHelper.LogGameWithoutCashOut(gameState.UserId, gameState.GameId, gameState.BetAmount, gameState.CurrentWinAmount, true);
                await _balanceService.WinAmountAsync(gameState.UserId, gameState.GameId);

                currentGamesDict.TryRemove(request.SessionId, out gameState);
                return new BombasticGameResult
                {
                    IsExploded = false,
                    CurrentClickNumber = 20,
                    CurrentWinAmount = gameState.CurrentWinAmount,
                    CurrentMulitplier = gameState.CurrentMulitplier,
                    Message = "Tillykke du desarmeret bomben."
                };

            }
            else
            {
                gameState.CurrentClickNumber++;
                gameState.CurrentMulitplier++;

                Console.WriteLine($"BetAmount: {gameState.BetAmount}, Multiplier: {gameState.CurrentMulitplier}, WinAmount: {gameState.CurrentWinAmount}");
                gameState.CurrentWinAmount = gameState.BetAmount * gameState.CurrentMulitplier;

                return new BombasticGameResult
                {
                    IsExploded = false,
                    CurrentClickNumber = gameState.CurrentClickNumber,
                    CurrentWinAmount = gameState.CurrentWinAmount,
                    CurrentMulitplier = gameState.CurrentMulitplier,
                    Message = "Du er safe! - Bliver du ved eller casher du ud?"
                };

            }

        }
    }
}
