using FoodOrdering.Common.Models;
using FoodOrdering.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    /// <summary>
    /// Provides administrative operations for managing food items.
    /// Only users with Admin role are authorized to access these APIs.
    /// </summary>
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;
        private readonly ILogger<AdminController> _logger;

        /// <summary>
        /// Initializes a new instance of the AdminController class.
        /// </summary>
        /// <param name="foodItemService">Food item service instance.</param>
        /// <param name="logger">Logger instance for recording application logs.</param>
        public AdminController(
            IFoodItemService foodItemService,
            ILogger<AdminController> logger)
        {
            _foodItemService = foodItemService;
            _logger = logger;
        }

        /// <summary>
        /// Asynchronously adds a new food item to the system.
        /// </summary>
        /// <param name="request">Food item details.</param>
        /// <returns>Success response containing operation result.</returns>
        [HttpPost]
        [Route("AddAsync")]
        public async Task<IActionResult> AddAsync(
            CreateFoodItemRequest request)
        {
            _logger.LogInformation("Add Food Item API called");

            var foodItem = new FoodItem
            {
                FoodName = request.FoodName,
                Category = request.Category,
                Price = request.Price
            };

            var result =
                await _foodItemService.AddFoodItem(foodItem);

            _logger.LogInformation(
                "Food Item added successfully");

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Item Added Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Asynchronously inserts multiple food items into the system.
        /// </summary>
        /// <param name="foodItems">List of food items.</param>
        /// <returns>Bulk insert result.</returns>
        [HttpPost]
        [Route("BulkInsertAsync")]
        public async Task<IActionResult> BulkInsertAsync(
            List<FoodItem> foodItems)
        {
            _logger.LogInformation("Bulk Insert API called");

            var result =
                await _foodItemService
                    .BulkInsertFoodItems(foodItems);

            _logger.LogInformation(
                "Bulk Insert completed successfully");

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Bulk Insert Completed Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Asynchronously updates an existing food item.
        /// </summary>
        /// <param name="foodItem">Updated food item information.</param>
        /// <returns>Update result.</returns>
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<IActionResult> UpdateAsync(
            FoodItem foodItem)
        {
            _logger.LogInformation(
                "Update Food Item API called");

            var result =
                await _foodItemService
                    .UpdateFoodItem(foodItem);

            _logger.LogInformation(
                "Food Item updated successfully");

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Item Updated Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Asynchronously deletes a food item using its GUID.
        /// </summary>
        /// <param name="foodItemGuid">Food Item GUID.</param>
        /// <returns>Delete result.</returns>
        [HttpDelete]
        [Route("DeleteByGuidAsync/{foodItemGuid}")]
        public async Task<IActionResult> DeleteByGuidAsync(
            Guid foodItemGuid)
        {
            _logger.LogInformation(
                "Delete Food Item By Guid API called for Guid: {FoodItemGuid}",
                foodItemGuid);

            var result =
                await _foodItemService
                    .DeleteFoodItemByGuid(
                        foodItemGuid);

            _logger.LogInformation(
                "Food Item deleted successfully using Guid");

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Item Deleted Successfully Using Guid",
                Data = result
            });
        }
    }
}