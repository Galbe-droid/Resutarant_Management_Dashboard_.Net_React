using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Repository;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Template_restaurant_app.Application.Services
{
    public class RestaurantTableService : IRestaurantTableService
    {
        private readonly RestaurantDbContext _context;
        public RestaurantTableService(RestaurantDbContext context)
        {
            _context = context;
        }
        public async Task<Result<List<ReturnTableDto>>> GetAllAsync(Guid userId)
        {
            _context.CurrentUserId = userId;

            var allTables = await _context.RestaurantTables.AsNoTracking().ToListAsync();

            return Result<List<ReturnTableDto>>.Ok(RestaurantTableMapping.ToReturnTables(allTables));
        }

        public async Task<Result<ReturnTableDto>> GetByIdAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;
            
            var table = await _context.RestaurantTables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);

            if(table == null)
            {
                return Result<ReturnTableDto>.Fail("Table not find.");
            }

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<ReturnTableDto>> CreateAsync(CreateTableDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            if(_context.RestaurantTables.Any(t => t.Number == create.Number))
            {
                return Result<ReturnTableDto>.Fail("Number already used.");
            }

            var table = RestaurantTableMapping.ToRestaurantTable(create);

            await _context.RestaurantTables.AddAsync(table);
            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<ReturnTableDto>> UpdateAsync(Guid id, UpdateTableDto update, Guid userId)
        {
            _context.CurrentUserId = userId;          

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<ReturnTableDto>.Fail("Table not found.");
            }

            table = RestaurantTableMapping.ToRestaurantTable(table, update);

            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));

        }

        public async Task<Result<ReturnTableDto>> ReservationAsync(Guid id, Guid userId, ChangeTableStatusDto change)
        {
            _context.CurrentUserId = userId;

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<ReturnTableDto>.Fail("Table not found.");
            }

            table = RestaurantTableMapping.ToRestaurantTable(table, change);

            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;
            
            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<bool>.Fail("Table not found.");
            }

            _context.RestaurantTables.Remove(table);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
