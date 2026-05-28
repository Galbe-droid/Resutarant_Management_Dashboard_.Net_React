using System.ComponentModel.DataAnnotations;

namespace Template_restaurant_app.Application.Dtos.Table
{
    public class UpdateTableDto
    {
        [Required]
        public int Number { get; set; }
        [Required]
        public int Capacity { get; set; }
    }
}
