using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Product;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly RestaurantDbContext _context;
        public ProductService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ReturnProductDto>>> GetAllAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            var products = await _context.Products.AsNoTracking().ToListAsync();

            return Result<List<ReturnProductDto>>.Ok(ProductMapping.ToReturnProductDtos(products));
        }
        public async Task<Result<ReturnProductDto>> GetByIdAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return Result<ReturnProductDto>.Fail("Product not found");
            }

            return Result<ReturnProductDto>.Ok(ProductMapping.ToReturnProduct(product));
        }
        public async Task<Result<ReturnProductDto>> CreateAsync(CreateProductDto create, Guid userId)
        {
            _context.CurrentUserId = userId;
            
            if(_context.Products.Any(p => p.Name == create.Name))
            {
                return Result<ReturnProductDto>.Fail("Product with the same name already exists");
            }

            var category = _context.Categories.FirstOrDefault(c => c.Id == create.CategoryId);

            if(category == null)
            {
                category = _context.Categories.FirstOrDefault(c => c.Name == "Other");
            }

            var product = ProductMapping.ToProduct(create, category!);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Result<ReturnProductDto>.Ok(ProductMapping.ToReturnProduct(product));
        }
        public async Task<Result<ReturnProductDto>> UpdateAsync(Guid id, UpdateProductDto update, Guid userId)
        {
            _context.CurrentUserId = userId;

            if (_context.Products.Any(p => p.Name == update.Name && p.Id != id))
            {
                return Result<ReturnProductDto>.Fail("Product with the same name already exists");
            }

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return Result<ReturnProductDto>.Fail("Product not found");
            }          

            var category = _context.Categories.FirstOrDefault(c => c.Id == update.CategoryId);

            product = ProductMapping.ToProduct(product, update, category);
            await _context.SaveChangesAsync();

            return Result<ReturnProductDto>.Ok(ProductMapping.ToReturnProduct(product));
        }
        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return Result<bool>.Fail("Product not found");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
