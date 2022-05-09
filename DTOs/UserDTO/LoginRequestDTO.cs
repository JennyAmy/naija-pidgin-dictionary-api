using System.ComponentModel.DataAnnotations;

namespace NaijaPidginAPI.DTOs.UserDTO
{
    public class LoginRequestDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
