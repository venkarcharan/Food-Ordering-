namespace FoodOrdering.Common.Models
{
    /// <summary>
    /// Request model used for creating a food item.
    /// </summary>
    public class CreateFoodItemRequest
    {
        public string? FoodName { get; set; }

        public string? Category { get; set; }

        public decimal Price { get; set; }
    }
}