using Template_restaurant_app.Application.Dtos.OrderItem;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IOrderItemService
    {
        public Task<List<ReturnOrderItemDto>> GetAllAsync(Guid userId);
        public Task<ReturnOrderItemDto> GetByIdAsync(Guid id, Guid userId);
        public Task<ReturnOrderItemDto> CreateAsync(CreateOrderItemDto create, Guid userId);
        public Task<ReturnOrderItemDto> UpdateAsync(Guid id, UpdateOrderItemDto update, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
