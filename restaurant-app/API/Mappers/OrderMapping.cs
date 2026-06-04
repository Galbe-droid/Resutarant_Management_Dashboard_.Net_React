using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.API.Mappers
{
    public static class OrderMapping
    {
        public static Order ToOrder(RestaurantTable table)
        {
            return new Order
            {
                TableId = table.Id,
            };
        }

        public static Order ToUpdateOrder(Order order, Guid update)
        {
            order.TableId = update;

            return order;
        }
        public static ReturnOrderDto ToReturnOrder(Order order)
        {
            return new ReturnOrderDto
            {
                Id = order.Id,
                TableId = order.TableId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                OrderItems = OrderItemMapping.ToReturnOrderItems(order.OrderItems.ToList())
            };
        }

        public static List<ReturnOrderDto> ToReturnOrders(List<Order> orders)
        {
            return orders.Select(o => ToReturnOrder(o)).ToList();
        }
    }
}
