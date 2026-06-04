using Microsoft.AspNetCore.Http.HttpResults;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class OrderItemMapping
    {
        public static OrderItem ToOrderItem(ReceiveOrderItemDto create, Order order, Product product)
        {
            return new OrderItem
            {
                OrderId = order.Id,
                Order = order,
                ProductId = product.Id,
                Product = product,
                ProductName = product.Name,
                Quantity = create.Quantity,
                TotalPrice = product.Price * create.Quantity
            };
        }
        public static OrderItem ToUpdateOrderItem(OrderItem orderItem, ReceiveOrderItemDto update, Product product)
        {
            orderItem.OrderId = orderItem.OrderId;
            orderItem.ProductId = product.Id;
            orderItem.ProductName = product.Name;
            orderItem.Quantity = update.Quantity;
            orderItem.TotalPrice = product.Price * update.Quantity;

            return orderItem;
        }
        public static ReturnOrderItemDto ToReturnOrderItem(OrderItem orderItem)
        {
            return new ReturnOrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                ProductId = orderItem.ProductId,
                ProductName = orderItem.ProductName,
                Quantity = orderItem.Quantity,
                TotalPrice = orderItem.TotalPrice
            };
        }

        public static List<ReturnOrderItemDto> ToReturnOrderItems(List<OrderItem> orderItems)
        {
            return orderItems.Select(oi => ToReturnOrderItem(oi)).ToList();
        }
    }
}
