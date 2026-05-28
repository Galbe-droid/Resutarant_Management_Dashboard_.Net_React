using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Payment
{
    public class UpdatePaymentDto
    {
        public Guid OrderId { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
    }
}
