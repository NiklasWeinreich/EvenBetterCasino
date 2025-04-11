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

        [Column(TypeName = "int")]
        public int ImageId { get; set; }

        [Column(TypeName = "bool")]
        public required bool Status { get; set; } = false;
    }
}
