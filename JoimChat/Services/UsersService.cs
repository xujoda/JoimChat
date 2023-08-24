using JoimChat.Models;
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

        public async Task<User> CreateUserAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var newUser = await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return newUser.Entity;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            if (userId < 0) 
            {
                throw new ArgumentNullException($"userID {userId}");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == userId);

            return user ?? throw new ArgumentNullException($"userID {userId}");
        }

        public async Task<User> GetUserByNameAsync(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException($"{nameof(name)}");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == name);

            return user ?? throw new ArgumentNullException($"{nameof(name)}");
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            if (email == null)
            {
                throw new ArgumentOutOfRangeException($"{nameof(email)}");
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == email);

            return user ?? throw new ArgumentOutOfRangeException($"{nameof(email)}"); ;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            if (!await _context.Users.AnyAsync())
            {
                throw new ArgumentNullException("Users list is empty.");
            }

            var users = await _context.Users.ToListAsync();

            return users ?? throw new ArgumentNullException("Users list is empty.");
        }

        public async Task<ResponseService> DeleteUserByIdAsync(int userId)
        {
            var user = GetUserByIdAsync(userId);

            _context.Users.Remove(user.Result);
            await _context.SaveChangesAsync();

            return ResponseService.Success("User had been deleted");
        }

        public async Task<ResponseService> UpdateUserAsync(int userId, User user)
        {
            if (user == null)
            {
                return ResponseService.Success("User == null");
            }

            var originalUser = await GetUserByIdAsync(userId);

            if (originalUser == null)
            {
                return ResponseService.Error("Original user not found");
            }

            originalUser.SentMessages = user.SentMessages;
            originalUser.IsOnline = user.IsOnline;
            originalUser.LastOnline = user.LastOnline;
            originalUser.Email = user.Email;
            originalUser.Name = user.Name;
            originalUser.Password = user.Password;

            await _context.SaveChangesAsync();

            return ResponseService.Success("User has been updated");
        }
    }
}
