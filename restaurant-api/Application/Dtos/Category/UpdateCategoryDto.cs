using System.ComponentModel.DataAnnotations;

namespace Template_restaurant_app.Application.Dtos.Category
{
    public class UpdateCategoryDto
    {
        [Required]
        public string Name { get; set; }
    }
}
