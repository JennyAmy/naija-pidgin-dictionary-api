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

        [Required]
        public string Password { get; set; }
    }

    public class UpdateDTO
    {   
        public string Username { get; set; }
       
        public string Email { get; set; }
    }
}
