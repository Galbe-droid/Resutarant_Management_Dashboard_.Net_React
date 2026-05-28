using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Domain.Entities.UserRelated;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IProductService
    {
        public Task<List<ReturnProductDto>> GetAllAsync(Guid userId);
        public Task<ReturnProductDto> GetByIdAsync(Guid id, Guid userId);
        public Task<ReturnProductDto> CreateAsync(CreateProductDto create, Guid userId);
        public Task<ReturnProductDto> UpdateAsync(Guid id, UpdateProductDto update, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
