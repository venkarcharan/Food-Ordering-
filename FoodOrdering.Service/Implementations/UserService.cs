using FoodOrdering.Common.Models;
using FoodOrdering.Service.Abstractions;
using FoodOrdering.Store.Abstractions;

namespace FoodOrdering.Service.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserStore _userStore;

        public UserService(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userStore.GetUserByEmail(email);
        }
    }
}