using Backend.Database.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.GamesDTO
{
    public class GamesRequest
    {


        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "int")]
        public required int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? WebUrl { get; set; }

        [Column(TypeName = "decimal")]
        public decimal JackpotAmount { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? ImageUrl { get; set; }

        public required bool Status { get; set; } = false;

    }
}
