﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Database.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string FirstName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string LastName { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public required string Email { get; set; }

        [Column(TypeName = "nvarchar(500)")]
        public required string Password { get; set; }

        public required DateOnly BirthDate { get; set; }

        [Column(TypeName = "int")]
        public int? PhoneNumber { get; set; }

        [Column(TypeName = "int")]
        public int Balance { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ExcludedUntil { get; set; }

        [Column(TypeName = "int")]
        public int Profit { get; set; }

        [Column(TypeName = "int")]
        public int Loss { get; set; }

        public bool NewsLetterIsSubscribed { get; set; } = false;
    }
}
