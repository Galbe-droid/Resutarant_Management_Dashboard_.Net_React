using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Domain.Entities.UserRelated;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface IProductService
    {
        public Task<Result<List<ReturnProductDto>>> GetAllAsync(Guid userId);
        public Task<Result<ReturnProductDto>> GetByIdAsync(Guid id, Guid userId);
        public Task<Result<ReturnProductDto>> CreateAsync(CreateProductDto create, Guid userId);
        public Task<Result<ReturnProductDto>> UpdateAsync(Guid id, UpdateProductDto update, Guid userId);
        public Task<Result<bool>> DeleteAsync(Guid id, Guid userId);
    }
}
