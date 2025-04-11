using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class Categori
    {

        [Key]
        [Column(TypeName = "int")]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Name { get; set; }

        [Column(TypeName = "int")]
        public required string GamesId { get; set; }

    }
}
