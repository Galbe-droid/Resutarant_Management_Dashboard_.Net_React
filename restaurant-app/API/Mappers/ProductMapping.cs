using Microsoft.AspNetCore.Http.HttpResults;
using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.API.Mappers
{
    public static class ProductMapping
    {
        public static Product ToProduct(CreateProductDto create, Category category)
        {
            return new Product
            {
                Name = create.Name,
                Price = create.Price,
                Description = create.Description,
                ImageURL = create.ImageURL,
                CategoryId = category.Id,
                Category = category
            };
        }

        public static Product ToProduct(Product product, UpdateProductDto update, Category? category = null)
        {
            product.Name = update.Name;
            product.Price = update.Price;
            product.Description = update.Description;
            product.ImageURL = update.ImageURL;

            if(category != null)
            {
                product.CategoryId = category.Id;
                product.Category = category;
            }

            return product;
        }

        public static ReturnProductDto ToReturnProduct(Product product)
        {
            return new ReturnProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL,
                CategoryId = product.CategoryId,
                Category = CategoryMapping.ToReturnCategory(product.Category),
            };
        }

        public static ReturnToListProductDto ToReturnToListProductDto(Product product)
        {
            return new ReturnToListProductDto
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                ImageURL = product.ImageURL,
            };
        }

        public static List<ReturnToListProductDto> ToReturnToListProductDtos(ICollection<Product> products)
        {
            return (from Product product in products select ToReturnToListProductDto(product)).ToList();
        }
    }
}
