using System.ComponentModel.DataAnnotations;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Order
{
    public class CreateOrderDto
    {
        [Required]
        public Guid TableId { get; set; }
    }
}
