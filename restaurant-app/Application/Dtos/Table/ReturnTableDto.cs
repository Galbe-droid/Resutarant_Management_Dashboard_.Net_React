using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Table
{
    public class ReturnTableDto
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Capacity { get; set; }
        public TableStatus TableStatus { get; set; }
        public string? ReservationName { get; set; }
        public DateTimeOffset? ReservationTime { get; set; }
    }
}
