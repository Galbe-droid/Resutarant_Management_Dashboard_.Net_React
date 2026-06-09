using System.ComponentModel.DataAnnotations;

namespace Template_restaurant_app.Application.Dtos.OrderItem
{
    public class UpdateOrderItemDto
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}
