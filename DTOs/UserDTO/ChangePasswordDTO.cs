using System.ComponentModel.DataAnnotations;

namespace NaijaPidginAPI.DTOs.UserDTO
{
    public class ChangePasswordDTO
    {
        [Required(ErrorMessage = "Old Password is required")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "New Password is required")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is required")]
        public string ConfirmPassword { get; set; }
    }
}
