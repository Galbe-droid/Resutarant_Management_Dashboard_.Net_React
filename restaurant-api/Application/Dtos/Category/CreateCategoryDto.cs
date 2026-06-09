using System.ComponentModel.DataAnnotations;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.Application.Dtos.Category
{
    public class CreateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
