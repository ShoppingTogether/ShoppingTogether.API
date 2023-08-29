using Dapper;
using ShoppingTogether.API.Users.Models;
using System.Data;

namespace ShoppingTogether.API.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _connection;

        public UserRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var result = await _connection
                .QueryAsync<User>("SELECT * FROM get_all_users()");

            return result.ToList();
        }

        public async Task<User> GetUserBySidAsync(string sid)
        {
            if (string.IsNullOrEmpty(sid))
                return null!;

            return await _connection
                .QueryFirstOrDefaultAsync<User>("SELECT * FROM get_user_by_sid(@sid)",
                    new { sid = sid });
        }

        public async Task<User> AddUserAsync(string name, string sid)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(sid))
                return null!;

            return await _connection
                .QueryFirstOrDefaultAsync<User>("SELECT * FROM add_user(@name, @sid)",
                    new { name = name, sid = sid });
        }
    }
}
