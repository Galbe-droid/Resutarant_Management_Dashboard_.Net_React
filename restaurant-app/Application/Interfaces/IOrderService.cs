using Template_restaurant_app.Application.Dtos.Order;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<List<ReturnOrderDto>> GetAllAsync(Guid userId);
        public Task<ReturnOrderDto> GetByIdAsync(Guid id, Guid userId);
        public Task<ReturnOrderDto> CreateAsync(CreateOrderDto create, Guid userId);
        public Task<ReturnOrderDto> UpdateAsync(Guid id, UpdateOrderDto update, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
