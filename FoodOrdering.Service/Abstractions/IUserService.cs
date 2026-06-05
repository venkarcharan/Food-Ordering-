using FoodOrdering.Common.Models;

namespace FoodOrdering.Service.Abstractions
{
    public interface IUserService
    {
        Task<User> GetUserByEmail(string email);
    }
}