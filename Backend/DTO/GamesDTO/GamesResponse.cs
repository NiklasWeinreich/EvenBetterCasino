using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.GamesDTO
{
    public class GamesResponse
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int CategoryId { get; set; }
        public string Description { get; set; }
        public string? WebUrl { get; set; }
        public decimal JackpotAmount { get; set; }
        public string? ImageUrl { get; set; }
        public required bool Status { get; set; } = false;
    }
}
