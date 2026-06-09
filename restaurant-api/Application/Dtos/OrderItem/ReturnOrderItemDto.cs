using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.Product;

namespace Template_restaurant_app.Application.Dtos.OrderItem
{
    public class ReturnOrderItemDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
