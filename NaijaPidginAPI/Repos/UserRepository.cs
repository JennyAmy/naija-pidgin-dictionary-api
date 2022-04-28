using Microsoft.EntityFrameworkCore;
using NaijaPidginAPI.DbContexts;
using NaijaPidginAPI.DTOs.UserDTO;
using NaijaPidginAPI.Entities;
using NaijaPidginAPI.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace NaijaPidginAPI.Repos
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext context;

        public UserRepository(DataContext context)
        {
            this.context = context;
        }

        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {
            using(var hmac = new HMACSHA512(passwordKey))
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for(int i =0; i<passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                        return false;
                }
                return true;
            }
        }

        public void Register(RegisterDTO registerDTO)
        {
            byte[] passwordHash, passwordKey;

            using(var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(registerDTO.Password));
            }

            User user = new User();
            user.Username = registerDTO.Username;
            user.Email = registerDTO.Email;
            user.Password = passwordHash;
            user.PasswordKey = passwordKey;

            context.Users.Add(user);
        }

        public async Task<bool> UserAlreadyExists(string userName) ///For someone registering for the first time
        {
            return await context.Users.AnyAsync(x => x.Username == userName);
        }

        public async Task<bool> UserAlreadyExists(string userName, int? userId) //For a user trying to update his/her details.
        {
            return await context.Users.AnyAsync(x => x.Username == userName && x.UserId != userId);
        }

        public async Task<bool> UserDisabled(string userName)
        {
            return await context.Users.AnyAsync(x => x.Username == userName && x.IsDisabled == true);
        }

        public async Task<User> Authenticate(string userName, string passwordText)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.Username == userName);
            if(user == null || user.PasswordKey == null)
                return null;

            if (!MatchPasswordHash(passwordText, user.Password, user.PasswordKey))
                return null;

            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            var users = await context.Users
                .ToListAsync();

            return users;
        }
        public async Task<IEnumerable<User>> GetNonAdminsAsync()
        {
            var users = await context.Users
               .Where(u => u.IsAdmin == false && u.IsDisabled == false)
               .ToListAsync();

            return users;
        }

        public async Task<IEnumerable<User>> GetAdminsAsync()
        {
            var users = await context.Users
               .Where(u => u.IsAdmin == true && u.IsDisabled == false)
               .ToListAsync();

            return users;
        }


        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await context.Users
               .Where(u => u.UserId == id)
               .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> UserIsAdmin(int userId)
        {
            return await context.Users.AnyAsync(x => x.UserId == userId && x.IsAdmin == true && x.IsDisabled == false);
        }

        public int CountTotalUsers()
        {
            var totalUsers = context.Users.Count();

            return totalUsers;
        }
    }
}
