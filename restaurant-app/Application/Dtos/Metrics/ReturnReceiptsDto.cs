using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Metrics
{
    public class ReturnReceiptsDto
    {
        public Guid PaymentId { get; set; }
        public decimal Amount { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
