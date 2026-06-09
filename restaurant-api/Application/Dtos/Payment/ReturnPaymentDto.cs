using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Payment
{
    public class ReturnPaymentDto
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTimeOffset CreateAt { get; set; }
    }
}
