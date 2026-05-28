using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Domain.Entities
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid TableId { get; set; }
        public RestaurantTable Table { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.UtcNow;
        public decimal TotalAmount { get; set; } = 0;
        public OrderStatus Status { get; set; } = OrderStatus.Open;
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}
