using System;
using System.ComponentModel.DataAnnotations;

namespace NaijaPidginAPI.DTOs.UserDTO
{
    public class RegisterDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
