using FoodOrdering.Common.Models;
using FoodOrdering.Service.Abstractions;
using FoodOrdering.Store.Abstractions;

namespace FoodOrdering.Service.Implementations
{
    public class FoodItemService : IFoodItemService
    {
        private readonly IFoodItemStore _foodItemStore;

        public FoodItemService(
            IFoodItemStore foodItemStore)
        {
            _foodItemStore = foodItemStore;
        }

        public async Task<List<FoodItem>> GetAllFoodItems()
        {
            return await _foodItemStore
                .GetAllFoodItems();
        }

        public async Task<List<FoodItem>> GetFoodItemsPagination(
            int pageNumber,
            int pageSize)
        {
            return await _foodItemStore
                .GetFoodItemsPagination(
                    pageNumber,
                    pageSize);
        }

        public async Task<List<FoodItem>> GetFilteredFoodItems(
            string? foodName,
            string? category)
        {
            return await _foodItemStore
                .GetFilteredFoodItems(
                    foodName,
                    category);
        }

        public async Task<FoodItem> GetFoodItemByGuid(
            Guid foodItemGuid)
        {
            return await _foodItemStore
                .GetFoodItemByGuid(
                    foodItemGuid);
        }

        public async Task<int> AddFoodItem(
            FoodItem foodItem)
        {
            return await _foodItemStore
                .AddFoodItem(foodItem);
        }

        public async Task<int> UpdateFoodItem(
            FoodItem foodItem)
        {
            return await _foodItemStore
                .UpdateFoodItem(foodItem);
        }

        public async Task<int> DeleteFoodItemByGuid(
            Guid foodItemGuid)
        {
            return await _foodItemStore
                .DeleteFoodItemByGuid(
                    foodItemGuid);
        }

        public async Task<int> BulkInsertFoodItems(
            List<FoodItem> foodItems)
        {
            return await _foodItemStore
                .BulkInsertFoodItems(
                    foodItems);
        }
    }
}