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
                return Result<ReturnCategoryDto>.Fail("Category not found");
            }

            return Result<ReturnCategoryDto>.Ok(CategoryMapping.ToReturnCategory(category));
        }

        public async Task<Result<ReturnCategoryDto>> CreateAsync(CreateCategoryDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            if(_context.Categories.Any(c => c.Name == create.Name))
            {
                return Result<ReturnCategoryDto>.Fail("Category with the same name already exists");
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
                return Result<ReturnCategoryDto>.Fail("Category not found");
            }

            if(_context.Categories.Any(c => c.Name == update.Name && c.Id != id))
            {
                return Result<ReturnCategoryDto>.Fail("Category with the same name already exists");
            }

            category = await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);

            category = CategoryMapping.ToCategory(category!, update);

            await _context.SaveChangesAsync();

            return Result<ReturnCategoryDto>.Ok(CategoryMapping.ToReturnCategory(category));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if(category == null)
            {
                return Result<bool>.Fail("Category not found");
            }

            if (category.Name == "Other")
            {
                return Result<bool>.Fail("Cannot delete default category");
            }

            var otherCategoryId = await _context.Categories.Where(c => c.Name == "Other").Select(c => c.Id).FirstOrDefaultAsync();

            foreach (var product in category.Products)
            {
                product.CategoryId = otherCategoryId;
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
