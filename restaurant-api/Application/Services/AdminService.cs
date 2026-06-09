using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Template_restaurant_app.API.Mappers;
using Template_restaurant_app.Application.Common;
using Template_restaurant_app.Application.Dtos.Metrics;
using Template_restaurant_app.Application.Dtos.Order;
using Template_restaurant_app.Application.Dtos.OrderItem;
using Template_restaurant_app.Application.Dtos.User;
using Template_restaurant_app.Application.Interfaces;
using Template_restaurant_app.Domain.Constant;
using Template_restaurant_app.Domain.Entities.UserRelated;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly RestaurantDbContext _dBContext;
        private readonly PasswordHasher<User> _hasher;
        public AdminService(RestaurantDbContext dbContext, PasswordHasher<User> hasher)
        {
            _dBContext = dbContext;
            _hasher = new PasswordHasher<User>();
        }
        public async Task<Result<List<ReturnUser>>> GetAllUsersAsync(Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var users = await _dBContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Select(u => new ReturnUser
                {
                    Username = u.Username,
                    Name = u.Name,
                    Email = u.Email,
                    Roles = u.UserRoles.Select(ur => ur.Role.Name).ToList()
                })
                .ToListAsync();

            return Result<List<ReturnUser>>.Ok(users);    
        }

        public async Task<Result<List<ReturnOrderDto>>> GetRestaurantOrdersAsync(Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var orders = await _dBContext.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                    .Select(o => new ReturnOrderDto
                    {
                        Id = o.Id,
                        TableId = o.TableId,
                        OrderDate = o.OrderDate,
                        TotalAmount = o.TotalAmount,
                        Status = o.Status,
                        OrderItems = o.OrderItems.Select(oi => new ReturnOrderItemDto
                        {
                            Id = oi.Id,
                            OrderId = o.Id,
                            ProductId = oi.ProductId,
                            ProductName = oi.ProductName,
                            Quantity = oi.Quantity,
                            TotalPrice = oi.TotalPrice,
                        }).ToList()
                    }).ToListAsync();

            return Result<List<ReturnOrderDto>>.Ok(orders);
        }

        public async Task<Result<List<ReturnTotalAmountSoldOfProductDto>>> GetRestaurantPopularItemsAsync(Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var popularItems = _dBContext.OrderItems
                    .Include(oi => oi.Product)
                    .GroupBy(oi => new { oi.ProductId, oi.Product.Name })
                    .Select(g => new ReturnTotalAmountSoldOfProductDto
                    {
                        ProductId = g.Key.ProductId,
                        ProductName = g.Key.Name,
                        TotalAmountSold = g.Sum(oi => oi.Quantity),
                        TotalRevenue = g.Sum(oi => oi.TotalPrice)
                    })
                    .OrderByDescending(x => x.TotalAmountSold)
                    .ToListAsync();

            return Result<List<ReturnTotalAmountSoldOfProductDto>>.Ok(await popularItems);
        }

        public async Task<Result<List<ReturnReceiptsDto>>> GetRestaurantReceiptsAsync(Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var receipts = await _dBContext.Payments
                    .Select(p => new ReturnReceiptsDto
                    {
                        PaymentId = p.Id,
                        Amount = p.Amount,
                        PaymentMethod = p.PaymentMethod,
                        CreatedAt = p.PaymentDate
                    }).ToListAsync();

            return Result<List<ReturnReceiptsDto>>.Ok(receipts);    
        }

        public async Task<Result<ReturnUser>> GetUserByIdAsync(Guid id, Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var user = await _dBContext.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return Result<ReturnUser>.Fail("User not found");

            return Result<ReturnUser>.Ok(UserMapping.ToReturnUser(user));
        }
        public async Task<(bool Sucess, object? Errors)> CreateRestaurantAccountAsync(RegisterRestaurantUser register, Guid userId)
        {
            var errors = new Dictionary<string, string>();

            if (await _dBContext.Users.AnyAsync(x => x.Username == register.Username))
                errors["username"] = "Username already exists";

            if (await _dBContext.Users.AnyAsync(x => x.Email == register.Email))
                errors["email"] = "Email already exists";

            if(!await _dBContext.Roles.AnyAsync(r => r.Name.ToLower() == register.Role.ToLower()))
                errors["role"] = "Role not found";

            if (errors.Any())
                return (false, errors);

            var user = UserMapping.ToUser(register);

            Role userRole = await _dBContext.Roles.FirstOrDefaultAsync(r => r.Name.ToLower() == register.Role.ToLower());

            user.UserRoles.Add(new UserRole { RoleId = userRole!.Id, Role = userRole, UserId = user.Id, User = user });

            user.PasswordHash = _hasher.HashPassword(user, register.Password);

            await _dBContext.Users.AddAsync(user);

            await _dBContext.SaveChangesAsync();

            return (true, null);
        }

        public async Task<Result<bool>> DeleteFromAdminUserAsync(Guid id, Guid userId)
        {
            _dBContext.CurrentUserId = userId;

            var user = await _dBContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
                return Result<bool>.Fail("User not found");

            if(user.UserRoles.Any(ur => ur.Role.Name == Roles.Admin))
                return Result<bool>.Fail("Cannot delete an admin user");

            user.IsDeleted = true;
            await _dBContext.SaveChangesAsync();

            return Result<bool>.Ok(true);
        }
    }
}
