using System.ComponentModel.DataAnnotations;
using Template_restaurant_app.Domain.Enum;

namespace Template_restaurant_app.Application.Dtos.Table
{
    public class CreateTableDto
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
