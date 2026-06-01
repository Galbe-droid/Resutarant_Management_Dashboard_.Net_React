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
                Table = table
            };
        }

        public static Order ToUpdateOrder(Order order, RestaurantTable? update = null)
        {
            if(update != null)
            {
                order.Table.Number = update.Number;
            }
            return order;
        }
        public static ReturnOrderDto ToReturnOrder(Order order)
        {
            return new ReturnOrderDto
            {
                Id = order.Id,
                TableId = order.TableId,
                Table = RestaurantTableMapping.ToReturnTableDto(order.Table),
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
