using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FoodOrdering.Common.Models
{
    /// <summary>
    /// Represents a food item available in the food ordering system.
    /// Contains food details, pricing information, and audit information.
    /// </summary>
    public class FoodItem
    {
        /// <summary>
        /// Internal database identifier.
        /// Hidden from API responses.
        /// </summary>
        [JsonIgnore]
        public int FoodItemId { get; set; }

        /// <summary>
        /// Globally unique identifier of the food item.
        /// </summary>
        public Guid FoodItemGuid { get; set; }

        /// <summary>
        /// Name of the food item.
        /// </summary>
        [Required(ErrorMessage = "Food Name is required")]
        [StringLength(100, ErrorMessage = "Food Name cannot exceed 100 characters")]
        public string? FoodName { get; set; }

        /// <summary>
        /// Category to which the food item belongs.
        /// Example: Fast Food, Chinese, South Indian.
        /// </summary>
        [Required(ErrorMessage = "Category is required")]
        [StringLength(50, ErrorMessage = "Category cannot exceed 50 characters")]
        public string? Category { get; set; }

        /// <summary>
        /// Price of the food item.
        /// Must be between 1 and 10000.
        /// </summary>
        [Range(1, 10000, ErrorMessage = "Price must be between 1 and 10000")]
        public decimal Price { get; set; }

        /// <summary>
        /// Indicates whether the food item is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Date and time when the food item was created.
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// User who created the food item.
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date and time when the food item was last updated.
        /// </summary>
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// User who last updated the food item.
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}