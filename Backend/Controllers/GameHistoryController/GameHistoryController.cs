using Backend.Database.Entities;
using Backend.DTO.GamesHistoryDTO;
using Backend.Interfaces.IGamesHistory;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.GameHistoryController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameHistoryController : ControllerBase
    {

        private readonly IGameHistoryService _gameHistoryService;

        public GameHistoryController(IGameHistoryService gameHistoryService)
        {
            _gameHistoryService = gameHistoryService;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAllGameHistoryTicketAsync()
        {
            var ticket = await _gameHistoryService.GetAllGameHistoryTicketsAsync();
            return Ok(ticket);
        }

        // Get BY USER ID 
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetGameHistoryByUserIdAsync(int userId)
        {
            var ticket = await _gameHistoryService.GetGameHistoryByUserIdAsync(userId);
            if (!ticket?.Any() ?? true)
            {
                return NotFound($"No game history, with relation user id {userId}, was found.");
            }

            return Ok(ticket);
        }

        // Get BY GAME ID
        [HttpGet("games/{gameId}")]
        public async Task<IActionResult> GetGameHistoryByGameIdAsync(int gameId)
        {
            var ticket = await _gameHistoryService.GetGameHistoryByGameIdAsync(gameId);
            if (!ticket?.Any() ?? true)
            {
                return NotFound($"No game history, with relation user id {gameId}, was found.");
            }

            return Ok(ticket);
        }




        // USER ID && GAME ID
        [HttpGet("user/{userId}/game/{gameId}")]
        public async Task<IActionResult> GetGameHistoryByGameIdAndUserIdAsync(int userId, int gameId)
        {
            var ticket = await _gameHistoryService.GetGameHistoryByGameIdAndUserIdAsync(userId, gameId);
            if (!ticket?.Any() ?? true)
            {
                return NotFound($"No game history, with relation user id {userId} and gameId {gameId} was found.");
            }

            return Ok(ticket);
        }


        // USER ID && DATO





        // CREATE GH
        [HttpPost("createticket")]
        public async Task<IActionResult> CreateGameHistoryTicket([FromForm] GameHistoryRequest newGameHistoryTicket)
        {

            try
            {

                var createdticket = await _gameHistoryService.CreateGameHistoryTicketAsync(newGameHistoryTicket);
                return Ok($"Ticket with id {createdticket.GameHistoryId}, userId {createdticket.UserId} and gameId {createdticket.GameId}, was successfully created.");

            }
            catch (Exception ex)
            { 
                return Problem(ex.Message);
            }
        }
    }
}
