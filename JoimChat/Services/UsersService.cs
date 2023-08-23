using JoimChat.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace JoimChat.Services
{
    public class UsersService : IUsersService
    {
        private readonly ChatDbContext _context;

        public UsersService(ChatDbContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            if (userId < 0) 
            {
                throw new ArgumentOutOfRangeException(nameof(userId));
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return user;
        }

        public async Task<User> GetUserByName(string name)
        {
            if (name == null)
            {
                throw new ArgumentOutOfRangeException($"{nameof(name)}");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            if (email == null)
            {
                throw new ArgumentOutOfRangeException($"{nameof(email)}");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == email);

            if (user == null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            if (_context.Users.Count() <= 0)
            {
                throw new ArgumentNullException();
            }

            var users = await _context.Users.ToListAsync();

            if (users == null)
            {
                throw new ArgumentNullException();
            }

            return users;
        }

        public async Task DeleteUserById(int userId)
        {
            var user = GetUserById(userId);

            _context.Users.Remove(user.Result);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException();
            }

            var originalUser = await _context.Users.FirstOrDefaultAsync(u => user == u);

            if (originalUser == null)
            {
                throw new ArgumentNullException();
            }

            originalUser.SentMessages = user.SentMessages;
            originalUser.IsOnline = user.IsOnline;
            originalUser.LastOnline = user.LastOnline;
            originalUser.Email = user.Email;
            originalUser.Name = user.Name;
            originalUser.Password = user.Password;

            await _context.SaveChangesAsync();
        }
    }
}
