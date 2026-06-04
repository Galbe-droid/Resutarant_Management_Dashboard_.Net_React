using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Order
{
    public class ReturnOrderDto
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public List<ReturnOrderItemDto> OrderItems { get; set; } = new List<ReturnOrderItemDto>();
    }
}
