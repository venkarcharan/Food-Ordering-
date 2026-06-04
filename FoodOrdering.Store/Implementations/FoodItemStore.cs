using FoodOrdering.Common.Models;
using FoodOrdering.Store.Abstractions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace FoodOrdering.Store.Implementations
{
    public class FoodItemStore : IFoodItemStore
    {
        private readonly IConfiguration _configuration;

        public FoodItemStore(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<FoodItem> GetFoodItemByGuid(
    Guid foodItemGuid)
        {
            FoodItem? foodItem = null;

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_GetFoodItemByGuid",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@FoodItemGuid",
                foodItemGuid);

            SqlDataReader reader =
                await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                foodItem = new FoodItem
                {
                    FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                    FoodItemGuid = Guid.Parse(reader["FoodItemGuid"].ToString()!),
                    FoodName = reader["FoodName"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Price = Convert.ToDecimal(reader["Price"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                    CreatedBy = reader["CreatedBy"]?.ToString(),
                    UpdatedOn = reader["UpdatedOn"] == DBNull.Value
        ? null
        : Convert.ToDateTime(reader["UpdatedOn"]),
                    UpdatedBy = reader["UpdatedBy"] == DBNull.Value
        ? null
        : reader["UpdatedBy"].ToString()
                };
            }

            return foodItem!;
        }

        public async Task<int> DeleteFoodItemByGuid(
    Guid foodItemGuid)
        {
            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_DeleteFoodItemByGuid",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@FoodItemGuid",
                foodItemGuid);

            cmd.Parameters.AddWithValue(
                "@UpdatedBy",
                "Admin");

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<List<FoodItem>> GetAllFoodItems()
        {
            List<FoodItem> foodItems = new();

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand("usp_GetFoodItems", con);
            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                foodItems.Add(new FoodItem
                {
                    FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                    FoodItemGuid = Guid.Parse(reader["FoodItemGuid"].ToString()!),
                    FoodName = reader["FoodName"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Price = Convert.ToDecimal(reader["Price"]),
                    IsActive = Convert.ToBoolean(reader["IsActive"]),
                    CreatedOn = Convert.ToDateTime(reader["CreatedOn"]),
                    CreatedBy = reader["CreatedBy"]?.ToString(),
                    UpdatedOn = reader["UpdatedOn"] == DBNull.Value
                        ? null
                        : Convert.ToDateTime(reader["UpdatedOn"]),
                    UpdatedBy = reader["UpdatedBy"] == DBNull.Value
                        ? null
                        : reader["UpdatedBy"].ToString()
                });
            }

            return foodItems;
        }

        public async Task<List<FoodItem>> GetFoodItemsPagination(
            int pageNumber,
            int pageSize)
        {
            List<FoodItem> foodItems = new();

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_GetFoodItemsPagination",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PageNumber", pageNumber);
            cmd.Parameters.AddWithValue("@PageSize", pageSize);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                foodItems.Add(new FoodItem
                {
                    FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                    FoodName = reader["FoodName"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Price = Convert.ToDecimal(reader["Price"])
                });
            }

            return foodItems;
        }

        public async Task<List<FoodItem>> GetFilteredFoodItems(
            string? foodName,
            string? category)
        {
            List<FoodItem> foodItems = new();

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_GetFoodItemsFiltered",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue(
                "@FoodName",
                string.IsNullOrWhiteSpace(foodName)
                    ? DBNull.Value
                    : foodName);

            cmd.Parameters.AddWithValue(
                "@Category",
                string.IsNullOrWhiteSpace(category)
                    ? DBNull.Value
                    : category);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                foodItems.Add(new FoodItem
                {
                    FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                    FoodItemGuid = Guid.Parse(reader["FoodItemGuid"].ToString()!),
                    FoodName = reader["FoodName"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Price = Convert.ToDecimal(reader["Price"])
                });
            }

            return foodItems;
        }

        public async Task<FoodItem> GetFoodItemById(int id)
        {
            FoodItem? foodItem = null;

            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_GetFoodItemById",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FoodItemId", id);

            SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                foodItem = new FoodItem
                {
                    FoodItemId = Convert.ToInt32(reader["FoodItemId"]),
                    FoodItemGuid = Guid.Parse(reader["FoodItemGuid"].ToString()!),
                    FoodName = reader["FoodName"].ToString()!,
                    Category = reader["Category"].ToString()!,
                    Price = Convert.ToDecimal(reader["Price"])
                };
            }

            return foodItem!;
        }

        public async Task<int> AddFoodItem(FoodItem foodItem)
        {
            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_AddFoodItem",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FoodName", foodItem.FoodName);
            cmd.Parameters.AddWithValue("@Category", foodItem.Category);
            cmd.Parameters.AddWithValue("@Price", foodItem.Price);
            cmd.Parameters.AddWithValue("@CreatedBy", "Admin");

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateFoodItem(FoodItem foodItem)
        {
            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_UpdateFoodItem",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FoodItemId", foodItem.FoodItemId);
            cmd.Parameters.AddWithValue("@FoodName", foodItem.FoodName);
            cmd.Parameters.AddWithValue("@Category", foodItem.Category);
            cmd.Parameters.AddWithValue("@Price", foodItem.Price);
            cmd.Parameters.AddWithValue("@UpdatedBy", "Admin");

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteFoodItem(int id)
        {
            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            SqlCommand cmd = new SqlCommand(
                "usp_DeleteFoodItem",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@FoodItemId", id);
            cmd.Parameters.AddWithValue("@UpdatedBy", "Admin");

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> BulkInsertFoodItems(
            List<FoodItem> foodItems)
        {
            using SqlConnection con = new SqlConnection(
                _configuration.GetConnectionString("DefaultConnection"));

            await con.OpenAsync();

            DataTable dt = new DataTable();

            dt.Columns.Add("FoodName", typeof(string));
            dt.Columns.Add("Category", typeof(string));
            dt.Columns.Add("Price", typeof(decimal));

            foreach (var item in foodItems)
            {
                dt.Rows.Add(
                    item.FoodName,
                    item.Category,
                    item.Price);
            }

            SqlCommand cmd = new SqlCommand(
                "usp_BulkInsertFoodItems",
                con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlParameter tvpParam =
                cmd.Parameters.AddWithValue(
                    "@FoodItems",
                    dt);

            tvpParam.SqlDbType = SqlDbType.Structured;
            tvpParam.TypeName = "FoodItem_Type";

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}