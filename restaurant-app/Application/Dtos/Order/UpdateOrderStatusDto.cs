using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Order
{
    public class UpdateOrderStatusDto
    {       
        public OrderStatus? Status { get; set; }
    }
}
