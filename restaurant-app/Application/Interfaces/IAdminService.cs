using Template_restaurant_app.Application.Dtos.Metrics;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.Payment;
using Template_restaurant_app.Application.Dtos.User;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IAdminService
    {
        public Task<ReturnUser> CreateRestaurantAccountAsync(RegisterUser register, Guid userId);
        public Task<List<ReturnUser>> GetAllUsersAsync(Guid userId);
        public Task<ReturnUser> GetUserByIdAsync(Guid id, Guid userId);
        public Task DeleteUserAsync(Guid id, Guid userId);
        public Task<List<ReturnReceiptsDto>> GetRestaurantReceiptsAsync(Guid userId);
        public Task<List<ReturnOrderDto>> GetRestaurantOrdersAsync(Guid userId);
        public Task<List<ReturnTotalAmountSoldOfProductDto>> GetRestaurantPopularItemsAsync(Guid userId);
    }
}
