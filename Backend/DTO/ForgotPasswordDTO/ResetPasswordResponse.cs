﻿namespace Backend.DTO.ForgotPasswordDTO
{
    public class ResetPasswordResponse
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
    }
}
