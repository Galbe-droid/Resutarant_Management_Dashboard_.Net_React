using Template_restaurant_app.Application.Dtos.Category;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class CategoryMapping
    {
        public static Category ToCategory(CreateCategoryDto create)
        {
            return new Category
            {
                Name = create.Name,
            };
        }

        public static Category ToCategory(Category category, UpdateCategoryDto update)
        {
            category.Name = update.Name;
            return category;
        }

        public static ReturnCategoryDto ToReturnCategory(Category category)
        {
            return new ReturnCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Products = ProductMapping.ToReturnToListProductDtos(category.Products) 
            };
        }
    }
}
