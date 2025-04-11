using Backend.Helper;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.DTO.UserDTO
{
    public class UserRequest
    {
        [Required]
        [StringLength(32, ErrorMessage = "First name cannot be longer than 32 characters")]
        public string FirstName { get; set; } = default!;

        [Required]
        [StringLength(32, ErrorMessage = "Last name cannot be longer than 32 characters")]
        public string LastName { get; set; } = default!;

        [Column(TypeName = "nvarchar(500)")]
        public string? Password { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Email name cannot be longer than 50 characters")]
        public string Email { get; set; } = default!;

        [Required]
        public DateOnly BirthDate { get; set; } = default!;

        [Column(TypeName = "int")]
        public int? PhoneNumber { get; set; }

        public Role Role { get; set; }

        public bool NewsLetterIsSubscribed { get; set; } = false;
    }
}
