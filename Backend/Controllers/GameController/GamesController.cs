using Backend.Database.Entities;
using Backend.DTO.GamesDTO;
using Backend.Interfaces.IGames;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers.GameController
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGamesService _gamesService;
        public GamesController(IGamesService gamesService) 
        {
            _gamesService = gamesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {
            var games = await _gamesService.GetAllGamesAsync();
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult?> GetGamesByIdAsync(int id)
        {
            var game = await _gamesService.GetGameByIdAsync(id);
            if (game == null)
            {
                return NotFound($"Game, with id {id}, not found.");
            }

            return Ok(game);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateGameAsync([FromForm] GamesRequest request)
        {
            try
            {
                var game = await _gamesService.CreateGameAsync(request);

                // Indsæt tjek for at samme spil ikke kan tilføjes to gange, eventuelt på unik navn? 

                return Ok($"{game.Name}, with id {game.Id}, was successfully created.");

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }

        [HttpPut("{id}/update")]
        public async Task<IActionResult> UpdateGameAsync([FromRoute] int id, [FromForm] GamesRequest request)
        {
            var updatedGame = await _gamesService.UpdateGameAsync(id, request);
            if (updatedGame == null)
            {
                return NotFound($"Game, with {id}, not found.");
            }
            
            return Ok(new { message = $"Game, with id {id}, was successfully updated.", game =  updatedGame });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGameAsync(int id)
        {

            var isDeleted = await _gamesService.DeleteGameAsync(id);
            if (!isDeleted)
            {
                return NotFound($"Game, with {id}, not found.");
            }
            return Ok($"Game, with id {id}, was deleted successfully");

        }


    }
}
