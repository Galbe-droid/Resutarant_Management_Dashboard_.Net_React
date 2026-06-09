using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Category;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<Result<List<ReturnCategoryDto>>> GetAllAsync(Guid userId);
        public Task<Result<ReturnCategoryDto>> GetByIdAsync(Guid id, Guid userId);
        public Task<Result<ReturnCategoryDto>> CreateAsync(CreateCategoryDto create, Guid userId);
        public Task<Result<ReturnCategoryDto>> UpdateAsync(Guid id, UpdateCategoryDto update, Guid userId);
        public Task<Result<bool>> DeleteAsync(Guid id, Guid userId);
    }
}
