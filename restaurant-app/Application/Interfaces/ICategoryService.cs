using Template_restaurant_app.Application.Dtos.Category;

namespace Template_restaurant_app.Application.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<ReturnCategoryDto>> GetAllAsync(Guid userId);
        public Task<ReturnCategoryDto> GetByIdAsync(Guid id, Guid userId);
        public Task<ReturnCategoryDto> CreateAsync(CreateCategoryDto create, Guid userId);
        public Task<ReturnCategoryDto> UpdateAsync(Guid id, UpdateCategoryDto update, Guid userId);
        public Task DeleteAsync(Guid id, Guid userId);
    }
}
