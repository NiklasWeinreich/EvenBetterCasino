using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "int")]
        public required int CategoryId { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? WebUrl { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? ImageUrl { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string description { get; set; }

        public required bool Status { get; set; } = false;

        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<GameHistory> GameHistories { get; set; } = new List<GameHistory>();
    }
}
