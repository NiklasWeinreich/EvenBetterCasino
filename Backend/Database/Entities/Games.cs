using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class Games
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "int")]
        public required int KategoriId { get; set; }

        [Column(TypeName = "int")]
        public int JackpotAmount { get; set; }

        [Column(TypeName = "nvarchar(255)")]
        public string? ImageUrl { get; set; } 

        public required bool Status { get; set; } = false;

        [ForeignKey("KategoriId")]
        public Categori Categori { get; set; }

        public ICollection<GamesHistory> GamesHistories { get; set; }
    }
}
