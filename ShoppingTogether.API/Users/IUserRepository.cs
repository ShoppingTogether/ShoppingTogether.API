using ShoppingTogether.API.Users.Models;

namespace ShoppingTogether.API.Users
{
    public interface IUserRepository
    {
        public Task<List<User>> GetAllUsersAsync();
        public Task<User> GetUserBySidAsync(string sid);
        public Task<User> AddUserAsync(string name, string sid);
    }
}
