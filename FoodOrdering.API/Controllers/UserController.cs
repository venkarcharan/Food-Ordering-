using FoodOrdering.Common.Models;
using FoodOrdering.Service.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodOrdering.API.Controllers
{
    /// <summary>
    /// Provides food item retrieval operations for authenticated users.
    /// Both Admin and User roles are authorized to access these APIs.
    /// </summary>
    [Authorize(Roles = "Admin,User")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IFoodItemService _foodItemService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of UserController.
        /// </summary>
        /// <param name="foodItemService">Food item service instance.</param>
        /// <param name="logger">Logger instance.</param>
        public UserController(
            IFoodItemService foodItemService,
            ILogger<UserController> logger)
        {
            _foodItemService = foodItemService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all available food items.
        /// </summary>
        /// <returns>List of all food items.</returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            _logger.LogInformation(
                "Get All Food Items API called");

            var result =
                await _foodItemService.GetAllFoodItems();

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Items Retrieved Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Retrieves food items using pagination.
        /// </summary>
        /// <param name="pageNumber">Current page number.</param>
        /// <param name="pageSize">Number of records per page.</param>
        /// <returns>Paginated list of food items.</returns>
        [HttpGet]
        [Route("GetAllPagination")]
        public async Task<IActionResult> GetAllPagination(
            int pageNumber = 1,
            int pageSize = 10)
        {
            _logger.LogInformation(
                "Get Food Items Pagination API called");

            var result =
                await _foodItemService
                    .GetFoodItemsPagination(
                        pageNumber,
                        pageSize);

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Items Retrieved Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Retrieves food items based on filter criteria.
        /// </summary>
        /// <param name="foodName">Food name filter.</param>
        /// <param name="category">Food category filter.</param>
        /// <returns>Filtered list of food items.</returns>
        [HttpGet]
        [Route("GetFiltered")]
        public async Task<IActionResult> GetFiltered(
            string? foodName,
            string? category)
        {
            _logger.LogInformation(
                "Get Filtered Food Items API called");

            var result =
                await _foodItemService
                    .GetFilteredFoodItems(
                        foodName,
                        category);

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Filtered Food Items Retrieved Successfully",
                Data = result
            });
        }

        /// <summary>
        /// Retrieves a food item using its GUID value.
        /// </summary>
        /// <param name="foodItemGuid">Food Item GUID.</param>
        /// <returns>Food item details.</returns>
        [HttpGet]
        [Route("GetByGuid/{foodItemGuid}")]
        public async Task<IActionResult> GetByGuid(
            Guid foodItemGuid)
        {
            _logger.LogInformation(
                "Get Food Item By Guid API called");

            var result =
                await _foodItemService
                    .GetFoodItemByGuid(
                        foodItemGuid);

            return Ok(new ApiResponse<object>
            {
                StatusCode = 200,
                Message = "Food Item Retrieved Successfully",
                Data = result
            });
        }
    }
}