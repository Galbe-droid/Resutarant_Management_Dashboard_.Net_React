using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Metrics;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.User;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IAdminService
    {
        public Task<Result<ReturnUser>> CreateRestaurantAccountAsync(RegisterUser register, Guid userId);
        public Task<Result<List<ReturnUser>>> GetAllUsersAsync(Guid userId);
        public Task<Result<ReturnUser>> GetUserByIdAsync(Guid id, Guid userId);
        public Task<Result<bool>> DeleteUserAsync(Guid id, Guid userId);
        public Task<Result<List<ReturnReceiptsDto>>> GetRestaurantReceiptsAsync(Guid userId);
        public Task<Result<List<ReturnOrderDto>>> GetRestaurantOrdersAsync(Guid userId);
        public Task<Result<List<ReturnTotalAmountSoldOfProductDto>>> GetRestaurantPopularItemsAsync(Guid userId);
    }              
}
