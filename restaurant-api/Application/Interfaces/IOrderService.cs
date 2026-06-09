using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IOrderService
    {
        public Task<Result<List<ReturnOrderDto>>> GetAllAsync(Guid userId);
        public Task<Result<ReturnOrderDto>> GetByIdAsync(Guid id, Guid userId);
        public Task<Result<ReturnOrderDto>> CreateAsync(CreateOrderDto create, Guid userId);
        public Task<Result<ReturnOrderDto>> UpdateStatusAsync(Guid id, UpdateOrderStatusDto update, Guid userId);
        public Task<Result<ReturnOrderDto>> UpdateTableAsync(Guid id, UpdateOrderTableDto update, Guid userId);
        public Task<Result<ReturnOrderDto>> AddOrderItemsAsync(Guid id, ReceiveOrderItemDto update, Guid userId);
        public Task<Result<ReturnOrderDto>> UpdateOrderItemsAsync(Guid id, ReceiveOrderItemDto update, Guid userId);
        public Task<Result<ReturnOrderDto>> RemoveOrderItemsAsync(Guid id, Guid orderId, Guid userId);   
        public Task<Result<bool>> DeleteAsync(Guid id, Guid userId);
    }
}
