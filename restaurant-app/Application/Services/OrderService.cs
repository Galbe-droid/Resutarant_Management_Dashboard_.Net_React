using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Enum;
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

            var getTable = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == create.TableId);

            if (getTable == null)
            {
                return Result<ReturnOrderDto>.Fail("Table not found");
            }

            if(getTable.TableStatus == TableStatus.Occupied)
            {
                return Result<ReturnOrderDto>.Fail("Table is not available");
            }

            var order = OrderMapping.ToOrder(getTable);
            getTable.TableStatus = TableStatus.Occupied;
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

            var oldTable = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == order.TableId);

            if (oldTable == null)
            {
                return Result<ReturnOrderDto>.Fail("Current table not found");
            }

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == update.RestaurantTableId);

            if (table == null)
            {
                return Result<ReturnOrderDto>.Fail("New table not found");
            }

            oldTable.TableStatus = TableStatus.Available;
            table.TableStatus = TableStatus.Occupied;

            order = OrderMapping.ToUpdateOrder(order, table.Id);
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

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == update.itemId);

            if (product == null)
            {
                return Result<ReturnOrderDto>.Fail("Product not found");
            }

            var createOrderItem = OrderItemMapping.ToOrderItem(update, order, product);

            _context.OrderItems.Add(createOrderItem);
            await _context.SaveChangesAsync();

            await UpdateOrderTotalAmount(id);

            var updatedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);

            if(updatedOrder == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found after update");
            }

            return Result<ReturnOrderDto>.Ok(OrderMapping.ToReturnOrder(updatedOrder));
        }     

        public async Task<Result<ReturnOrderDto>> UpdateOrderItemsAsync(Guid orderId, ReceiveOrderItemDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

            if (order == null)
            {
                return Result<ReturnOrderDto>.Fail("Order not found");
            }

            var orderItem = await _context.OrderItems.FirstOrDefaultAsync(i => i.Id == update.itemId && i.OrderId == orderId);

            if (orderItem == null)
            {
                return Result<ReturnOrderDto>.Fail("Order item not found");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == orderItem.ProductId);

            if (product == null)
            {
                return Result<ReturnOrderDto>.Fail("Product not found");
            }

            orderItem = OrderItemMapping.ToUpdateOrderItem(orderItem, update, product);

            await _context.SaveChangesAsync();

            await UpdateOrderTotalAmount(orderId);

            var updatedOrder = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == orderId);

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
            await _context.SaveChangesAsync();

            await UpdateOrderTotalAmount(id);

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

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == order.TableId);

            if(table == null)
            {
                return Result<bool>.Fail("Table not found");
            }

            table.TableStatus = TableStatus.Available;
            order.Status = OrderStatus.Cancelled;

            await _context.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }

        private async Task UpdateOrderTotalAmount(Guid orderId)
        {
            var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);
            if (order != null)
            {
                order.TotalAmount = await _context.OrderItems.Where(i => i.OrderId == orderId).SumAsync(i => i.TotalPrice);
                await _context.SaveChangesAsync();
            }
        }
    }
}
