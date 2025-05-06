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

            // Navigation property til alle spil i kategorien
            public ICollection<Games> Games { get; set; } = new List<Games>();
        }
}
