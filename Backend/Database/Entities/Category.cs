using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{

        public class Category
        {
            [Key]
            [Column(TypeName = "int")]
            public int CategoryId { get; set; }

            [Column(TypeName = "nvarchar(50)")]
            public required string Name { get; set; }

            // Navigation property til alle spil i kategorien
            public ICollection<Game> Games { get; set; } = new List<Game>();
        }
}
