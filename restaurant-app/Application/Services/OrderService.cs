using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly RestaurantDbContext _context;
        public OrderService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<Result<List<ReturnOrderDto>>> GetAllAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            var orders = await _context.Orders.AsNoTracking().ToListAsync();

            return Result<List<ReturnOrderDto>>.Ok(OrderMapping.ToReturnOrders(orders));
        }

        public async Task<Result<ReturnOrderDto>> GetByIdAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.Id == id);
            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }
            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(order));
        }

        public async Task<Result<ReturnOrderDto>> CreateAsync(CreateOrderDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            var getTable = await _context.RestaurantTables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == create.TableId);

            if (getTable == null)
            {
                return Result<ReturnOrderDto>.Fail("Table not found");
            }

            var order = OrderMapping.ToOrder(getTable);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(order));
        }

        public async Task<Result<ReturnOrderDto>> UpdateStatusAsync(Guid id, UpdateOrderStatusDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            order.Status = update.Status ?? order.Status;
            await _context.SaveChangesAsync();
            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(order));
        }

        public async Task<Result<ReturnOrderDto>> UpdateTableAsync(Guid id, UpdateOrderTableDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            var table = await _context.RestaurantTables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == update.RestaurantTable.Id);

            if (table == null)
            {
                return Result<ReturnOrderDto>.Fail("Table not found");
            }

            order = OrderMapping.ToUpdateOrder(order, table);
            await _context.SaveChangesAsync();
            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(order));
        }

        public async Task<Result<ReturnOrderDto>> AddOrderItemsAsync(Guid id, ReceiveOrderItemDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == update.itemId);

            if (product == null)
            {
                return Result<ReturnOrderDto>.Fail("Product not found");
            }

            var createOrderItem = OrderItemMapping.ToOrderItem(update, order, product);

            order.TotalAmount = await _context.OrderItems.Where(i => i.OrderId == order.Id).SumAsync(i => i.TotalPrice);
            _context.OrderItems.Add(createOrderItem);
            await _context.SaveChangesAsync();

            var updatedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if(updatedOrder == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found after update");
            }

            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(updatedOrder));
        }     

        public async Task<Result<ReturnOrderDto>> UpdateOrderItemsAsync(Guid id, Guid itemId, int quantity, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == itemId && i.OrderId == id);

            if (orderItem == null)
            {
                return Result<ReturnOrderDto>.Fail("Order item not found");
            }
            
            orderItem.Quantity = quantity;
            order.TotalAmount = await _context.OrderItems.Where(i => i.OrderId == order.Id).SumAsync(i => i.TotalPrice);
            await _context.SaveChangesAsync();

            var updatedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if (updatedOrder != null)
            {
                return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(updatedOrder));
            }

            return Result<ReturnOrderDto>.Fail("Order not found after update");
        }

        public async Task<Result<ReturnOrderDto>> RemoveOrderItemsAsync(Guid id, Guid itemId, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == id);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == itemId && i.OrderId == id);

            if (orderItem == null)
            {
                return Result<ReturnOrderDto>.Fail("Order item not found");
            }

            _context.OrderItems.Remove(orderItem);
            order.TotalAmount = await _context.OrderItems.Where(i => i.OrderId == order.Id).SumAsync(i => i.TotalPrice);
            await _context.SaveChangesAsync();

            var updatedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if (updatedOrder != null)
            {
                return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(updatedOrder));
            }

            return Result<ReturnOrderDto>.Fail("Order not found after update");
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
            {
                return Result<bool>.Fail("Order not found");
            }

            order.Status = Domain.Enum.OrderStatus.Cancelled;
            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

    }
}
