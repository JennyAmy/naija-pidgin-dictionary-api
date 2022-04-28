using NaijaPidginAPI.DTOs.UserDTO;
using NaijaPidginAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<User> Authenticate(string userName, string password);
        Task<IEnumerable<User>> GetAdminsAsync();
        Task<IEnumerable<User>> GetNonAdminsAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task<bool> UserIsAdmin(int userId);
        void Register(RegisterDTO registerDTO);
        Task<bool> UserAlreadyExists(string userName, int? userId);
        Task<bool> UserDisabled(string userName);
        Task<bool> UserAlreadyExists(string userName);
        int CountTotalUsers();
    }
}
