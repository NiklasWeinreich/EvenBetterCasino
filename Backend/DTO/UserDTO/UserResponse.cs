using Backend.Helper;

namespace Backend.DTO.UserDTO
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        //public string Password { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateOnly BirthDate { get; set; }
        public int? PhoneNumber { get; set; }
        public bool NewsLetterIsSubscribed { get; set; } = false;
        public int Balance { get; set; }
        public int Profit { get; set; }
        public int Loss { get; set; }
        public Role Role { get; set; }
        public DateTime? ExcludedUntil { get; set; }
    }
}
