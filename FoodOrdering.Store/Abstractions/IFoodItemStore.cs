using FoodOrdering.Common.Models;

namespace FoodOrdering.Store.Abstractions
{
    public interface IFoodItemStore
    {
        Task<List<FoodItem>> GetAllFoodItems();

        Task<List<FoodItem>> GetFoodItemsPagination(
            int pageNumber,
            int pageSize);

        Task<List<FoodItem>> GetFilteredFoodItems(
            string? foodName,
            string? category);

        Task<FoodItem> GetFoodItemByGuid(
            Guid foodItemGuid);

        Task<int> AddFoodItem(
            FoodItem foodItem);

        Task<int> UpdateFoodItem(
            FoodItem foodItem);

        Task<int> DeleteFoodItemByGuid(
            Guid foodItemGuid);

        Task<int> BulkInsertFoodItems(
            List<FoodItem> foodItems);
    }
}