using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Order
{
    public class UpdateOrderDto
    {
        public RestaurantTable? RestaurantTable { get; set; }
        public List<CreateOrderItemDto>? OrderItems { get; set; }
        public OrderStatus? Status { get; set; }
    }
}
