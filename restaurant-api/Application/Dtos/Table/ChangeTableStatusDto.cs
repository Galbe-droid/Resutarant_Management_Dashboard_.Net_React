using System.ComponentModel.DataAnnotations;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Table
{
    public class ChangeTableStatusDto
    {
        [Required]
        public TableStatus Status { get; set; }
        public string? ReservationName { get; set; }
        public DateTimeOffset? ReservationTime { get; set; }
    }
}
