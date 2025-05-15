using Backend.Database.Entities;
using Backend.DTO.GamesDTO;
using Backend.Interfaces.IGames;

namespace Backend.Services.GamesService
{
    public class GamesService : IGamesService
    {

        private readonly IGamesRepository _gamesRepository;

        public GamesService(IGamesRepository gamesRepository)
        {
            _gamesRepository = gamesRepository;
        }

        public async Task<List<GamesResponse>> GetAllGamesAsync()
        {

            var games = await _gamesRepository.GetAllGamesAsync();
            return games.Select(MapEntityToResponse).ToList();

        }

        public async Task<GamesResponse?> GetGameByIdAsync(int id)
        {

            var game = await _gamesRepository.GetGameByIdAsync(id);
            if (game == null) return null;

            return MapEntityToResponse(game);
        }

        public async Task<GamesResponse> CreateGameAsync(GamesRequest request)
        {
            var newGame = MapRequestToEntity(request);
            var createdGame = await _gamesRepository.CreateGameAsync(newGame);

            return MapEntityToResponse(createdGame);

        }

        public async Task<bool> DeleteGameAsync(int id)
        {
            var deletedGame = await _gamesRepository.DeleteGameAsync(id);
            return deletedGame;
        }

        public async Task<GamesResponse> UpdateGameAsync(int id, GamesRequest updateGame)
        {

            var existingGame = await _gamesRepository.GetGameByIdAsync(id);
            if (existingGame == null) throw new Exception($"Game, with id {id}, was not found");

            existingGame.Name = updateGame.Name;
            existingGame.CategoryId = updateGame.CategoryId;
            existingGame.WebUrl = updateGame.WebUrl;
            existingGame.ImageUrl = updateGame.ImageUrl;
            existingGame.Status = updateGame.Status;

            var updatedGame = await _gamesRepository.UpdateGameAsync(existingGame);
            return MapEntityToResponse(updatedGame);

    }

        public Game MapRequestToEntity(GamesRequest request)
        {
            var user = new Game
            {

                Name = request.Name,
                CategoryId = request.CategoryId,
                WebUrl = request.WebUrl,
                ImageUrl = request.ImageUrl,
                Status = request.Status,

            };

            return user;
        }

        public static GamesResponse MapEntityToResponse(Game response)
        {
            return new GamesResponse
            {
                
                Id = response.GameId,
                Name = response.Name,
                CategoryId = response.CategoryId,
                WebUrl = response.WebUrl,
                ImageUrl = response.ImageUrl,
                Status = response.Status,

            };
        }
    }
}
