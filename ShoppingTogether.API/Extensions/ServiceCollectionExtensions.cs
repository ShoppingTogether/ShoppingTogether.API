using Npgsql;
using ShoppingTogether.API.Users;
using System.Data;

namespace ShoppingTogether.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Allow snake case mappings i.e. (column_name) to (columnName)
            Dapper.DefaultTypeMap.MatchNamesWithUnderscores = true;

            var connectionString = configuration["ShoppingDb:ConnectionString"] ?? string.Empty;
            services.AddTransient<IDbConnection>(db => new NpgsqlConnection(connectionString));

            services.AddTransient<IUserRepository, UserRepository>();

            return services;
        }
    }
}
