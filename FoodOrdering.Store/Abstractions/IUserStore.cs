using FoodOrdering.Common.Models;

namespace FoodOrdering.Store.Abstractions
{
    public interface IUserStore
    {
        Task<User> GetUserByEmail(string email);
    }
}