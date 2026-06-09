using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.Application.Dtos.Order
{
    public class UpdateOrderTableDto
    {
        public Guid? RestaurantTableId { get; set; }
    }
}
