using System.Globalization;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Domain.Entities
{
    public class RestaurantTable
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public int Number { get; set; }
        public int Capacity { get; set; }
        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public TableStatus TableStatus { get; set; } = TableStatus.Available;
        public string? ReservationName { get; set; }
        public DateTimeOffset? ReservationTime { get; set; }
    }
}
