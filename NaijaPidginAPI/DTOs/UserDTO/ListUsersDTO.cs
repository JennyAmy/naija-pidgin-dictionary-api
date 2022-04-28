namespace NaijaPidginAPI.DTOs.UserDTO
{
    public class ListUsersDTO
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsDisabled { get; set; }
    }
}
