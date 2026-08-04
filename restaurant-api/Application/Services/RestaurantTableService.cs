using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Table;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Enum;
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
                return Result<ReturnTableDto>.NotFound("Table not find.");
            }

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<ReturnTableDto>> CreateAsync(CreateTableDto create, Guid userId)
        {
            _context.CurrentUserId = userId;

            if(_context.RestaurantTables.Any(t => t.Number == create.Number))
            {
                return Result<ReturnTableDto>.Validation("Number already used.");
            }

            if (create.Capacity <= 0)
            {
                return Result<ReturnTableDto>.Validation("Capacity cant be zero");
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
                return Result<ReturnTableDto>.NotFound("Table not found.");
            }

            if(update.Number != table.Number)
            {
                if (_context.RestaurantTables.Any(t => t.Number == update.Number))
                {
                    return Result<ReturnTableDto>.Validation("Number already used.");
                }
            }            

            if (update.Capacity <= 0)
            {
                return Result<ReturnTableDto>.Validation("Capacity cant be zero");
            }

            table = RestaurantTableMapping.ToRestaurantTable(table, update);

            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));

        }

        public async Task<Result<ReturnTableDto>> StatusAsync(Guid id, Guid userId, ChangeTableStatusDto status)
        {
            _context.CurrentUserId = userId;

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<ReturnTableDto>.NotFound("Table not found.");
            }   

            table = RestaurantTableMapping.ToRestaurantTable(table, status);

            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<ReturnTableDto>> ReservationAsync(Guid id, Guid userId, ResevationTableDto reservation)
        {
            _context.CurrentUserId = userId;

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<ReturnTableDto>.NotFound("Table not found.");
            }

            table = RestaurantTableMapping.ToRestaurantTable(table, reservation);

            table.TableStatus = TableStatus.Reserved;

            await _context.SaveChangesAsync();

            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }
        
        public async Task<Result<ReturnTableDto>> CancelReservationAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;

            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<ReturnTableDto>.NotFound("Table not found.");
            }

            var change = new ResevationTableDto
            {
                ReservationName = null,
                ReservationTime = null
            };

            table = RestaurantTableMapping.ToRestaurantTable(table, change);
            table.TableStatus = TableStatus.Available;
            await _context.SaveChangesAsync();
            return Result<ReturnTableDto>.Ok(RestaurantTableMapping.ToReturnTableDto(table));
        }

        public async Task<Result<bool>> DeleteAsync(Guid id, Guid userId)
        {
            _context.CurrentUserId = userId;
            
            var table = await _context.RestaurantTables.FirstOrDefaultAsync(t => t.Id == id);

            if (table == null)
            {
                return Result<bool>.NotFound("Table not found.");
            }

            _context.RestaurantTables.Remove(table);
            await _context.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
