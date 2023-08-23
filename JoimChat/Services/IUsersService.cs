using JoimChat.Models;

namespace JoimChat.Services
{
    public interface IUsersService
    {
        Task CreateUser(User user);
        Task DeleteUserById(int userId);
        Task UpdateUser(User user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserByName(string name);
        Task<User> GetUserByEmail(string email);
        Task<List<User>> GetAllUsers();
    }
}
