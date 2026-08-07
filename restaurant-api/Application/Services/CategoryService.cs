using Microsoft.EntityFrameworkCore;
using Sprache;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Category;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly RestaurantDbContext _context;
        public CategoryService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ReturnCategoryDto>>> GetAllAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            return Result<List<ReturnCategoryDto>>.Ok(CategoryMapping.ToReturnToListCategoryDtos(categories));
        }

        public async Task<Result<ReturnCategoryDto>> GetByIdAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;
            
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return Result<ReturnCategoryDto>.NotFound("Category not found");
            }

            return Result<ReturnCategoryDto>.Ok(CategoryMapping.ToReturnCategory(category));
        }

        public async Task<Result<List<ReturnCategoryDashboardDto>>> GetDashboardAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            List<ReturnCategoryDashboardDto> dashboardDtos = new List<ReturnCategoryDashboardDto>();
            var categories = await _context.Categories.AsNoTracking().ToListAsync();

            foreach (var category in categories)
            {
                var productCount = await _context.Products.CountAsync(p => p.CategoryId == category.Id);
                var dashboardDto = CategoryMapping.ToReturnCategoryDashboard(category, productCount);
                dashboardDtos.Add(dashboardDto);
            }           

            return Result<List<ReturnCategoryDashboardDto>>.Ok(dashboardDtos);
        }

        public async Task<Result<ReturnCategoryDto>> CreateAsync(CreateCategoryDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            var normalizedName = create.Name.Trim();

            if (await _context.Categories.AnyAsync(c => c.Name.ToLower() == normalizedName.ToLower()))
            {
                return Result<ReturnCategoryDto>.Conflict("Category with the same name already exists");
            }

            var category = CategoryMapping.ToCategory(create);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Result<ReturnCategoryDto>.Ok(CategoryMapping.ToReturnCategory(category));
        }        

        public async Task<Result<ReturnCategoryDto>> UpdateAsync(Guid id, UpdateCategoryDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return Result<ReturnCategoryDto>.NotFound("Category not found");
            }

            if(_context.Categories.Any(c => c.Name == update.Name && c.Id != id))
            {
                return Result<ReturnCategoryDto>.Conflict("Category with the same name already exists");
            }

            category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

            category = CategoryMapping.ToCategory(category!, update);

            await _context.SaveChangesAsync();

            return Result<ReturnCategoryDto>.Ok(CategoryMapping.ToReturnCategory(category));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
            {
                return Result<bool>.NotFound("Category not found.");
            }

            if (category.Name == "Other")
            {
                return Result<bool>.Validation("Cannot delete default category.");
            }

            var otherCategory = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == "Other");

            if (otherCategory == null)
            {
                return Result<bool>.Fail("Default category not found.");
            }

            await _context.Products
                .Where(p => p.CategoryId == id)
                .ExecuteUpdateAsync(setters => setters
                    .SetProperty(p => p.CategoryId, otherCategory.Id));

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
