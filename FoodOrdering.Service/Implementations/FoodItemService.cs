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
            try
            {
                return await _foodItemStore
                    .GetAllFoodItems();
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - GetAllFoodItems: {ex.Message}");
            }
        }

        public async Task<List<FoodItem>> GetFoodItemsPagination(
            int pageNumber,
            int pageSize)
        {
            try
            {
                return await _foodItemStore
                    .GetFoodItemsPagination(
                        pageNumber,
                        pageSize);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - GetFoodItemsPagination: {ex.Message}");
            }
        }

        public async Task<List<FoodItem>> GetFilteredFoodItems(
            string? foodName,
            string? category)
        {
            try
            {
                return await _foodItemStore
                    .GetFilteredFoodItems(
                        foodName,
                        category);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - GetFilteredFoodItems: {ex.Message}");
            }
        }

        public async Task<FoodItem> GetFoodItemByGuid(
            Guid foodItemGuid)
        {
            try
            {
                return await _foodItemStore
                    .GetFoodItemByGuid(
                        foodItemGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - GetFoodItemByGuid: {ex.Message}");
            }
        }

        public async Task<int> AddFoodItem(
            FoodItem foodItem)
        {
            try
            {
                return await _foodItemStore
                    .AddFoodItem(foodItem);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - AddFoodItem: {ex.Message}");
            }
        }

        public async Task<int> UpdateFoodItem(
            FoodItem foodItem)
        {
            try
            {
                return await _foodItemStore
                    .UpdateFoodItem(foodItem);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - UpdateFoodItem: {ex.Message}");
            }
        }

        public async Task<int> DeleteFoodItemByGuid(
            Guid foodItemGuid)
        {
            try
            {
                return await _foodItemStore
                    .DeleteFoodItemByGuid(
                        foodItemGuid);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - DeleteFoodItemByGuid: {ex.Message}");
            }
        }

        public async Task<int> BulkInsertFoodItems(
            List<FoodItem> foodItems)
        {
            try
            {
                return await _foodItemStore
                    .BulkInsertFoodItems(
                        foodItems);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    $"Service Error - BulkInsertFoodItems: {ex.Message}");
            }
        }
    }
}