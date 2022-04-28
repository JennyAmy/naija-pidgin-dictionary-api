using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NaijaPidginAPI.Entities
{
    public class User : BaseEntity
    {
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public byte[] Password { get; set; }
        public byte[] PasswordKey { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDisabled { get; set; }
        public int? DisabledBy { get; set; }
        public DateTime DateTimeCreated { get; set; } = DateTime.Now;
    }
}
