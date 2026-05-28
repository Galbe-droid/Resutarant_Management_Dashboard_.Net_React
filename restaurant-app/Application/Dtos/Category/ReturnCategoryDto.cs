using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.Application.Dtos.Category
{
    public class ReturnCategoryDto
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public List<ReturnToListProductDto> Products { get; set; }
    }
}
