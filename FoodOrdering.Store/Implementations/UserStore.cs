using FoodOrdering.Common.Models;
using FoodOrdering.Store.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FoodOrdering.Store.Implementations
{
    public class UserStore : IUserStore
    {
        private readonly IConfiguration _configuration;

        public UserStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            User? user = null;

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_GetUserByEmail",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@Email", email);

            SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new User
                {
                    UserId =
                        Convert.ToInt32(reader["UserId"]),

                    UserGuid =
                        Guid.Parse(reader["UserGuid"].ToString()!),

                    UserName =
                        reader["UserName"].ToString()!,

                    Email =
                        reader["Email"].ToString()!,

                    PasswordHash =
                        reader["PasswordHash"].ToString()!,

                    RoleId =
                        Convert.ToInt32(reader["RoleId"]),

                    IsActive =
                        Convert.ToBoolean(reader["IsActive"])
                };
            }

            return user!;
        }
    }
}