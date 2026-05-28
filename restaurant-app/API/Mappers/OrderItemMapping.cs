using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class OrderItemMapping
    {
        public static OrderItem ToOrderItem(CreateOrderItemDto create, Order order, Product product)
        {
            return new OrderItem
            {
                OrderId = order.Id,
                Order = order,
                ProductId = product.Id,
                Product = product,
                ProductName = product.Name,
                Quantity = create.Quantity
            };
        }

        public static OrderItem ToUpdateOrderItem(OrderItem orderItem, UpdateOrderItemDto update, Order? order = null, Product? product = null)
        {
            orderItem.OrderId = order == null ? orderItem.OrderId : order.Id;
            orderItem.Order = order == null ? orderItem.Order : order;
            orderItem.ProductId = product == null ? orderItem.ProductId : product.Id;
            orderItem.Product = product == null ? orderItem.Product : product;
            orderItem.ProductName = product == null ? orderItem.ProductName : product.Name;
            orderItem.Quantity = update.Quantity;

            return orderItem;
        }

        public static ReturnOrderItemDto ToReturnOrderItem(OrderItem orderItem)
        {
            return new ReturnOrderItemDto
            {
                Id = orderItem.Id,
                OrderId = orderItem.OrderId,
                Order = OrderMapping.ToReturnOrder(orderItem.Order),
                ProductId = orderItem.ProductId,
                Product = ProductMapping.ToReturnProduct(orderItem.Product),
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
