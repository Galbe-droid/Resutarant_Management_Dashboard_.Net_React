using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Payment
{
    public class CreatePaymentDto
    {
        public Guid OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
