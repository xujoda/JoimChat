using JoimChat.Models;

namespace JoimChat.Services
{
    public interface IUsersService
    {
        Task<User> CreateUserAsync(User user);
        Task<ResponseService> DeleteUserByIdAsync(int userId);
        Task<ResponseService> UpdateUserAsync(int userId, User user);
        Task<User> GetUserByIdAsync(int userId);
        Task<User> GetUserByNameAsync(string name);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
    }
}
