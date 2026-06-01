using System.ComponentModel.DataAnnotations;

namespace Template_restaurant_app.Application.Dtos.Product
{
    public class CreateProductDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public Guid CategoryId { get; set; }
}
}
