using Template_restaurant_app.Application.Dtos.Category;

namespace Template_restaurant_app.Application.Dtos.Product
{
    public class ReturnProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string? ImageURL { get; set; }
        public Guid CategoryId { get; set; }
        public ReturnCategoryDto Category { get; set; }
    }
}
