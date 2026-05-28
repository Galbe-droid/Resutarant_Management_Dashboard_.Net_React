using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using Template_restaurant_app.Domain.Constant;
using Template_restaurant_app.Domain.Entities;
using Template_restaurant_app.Domain.Entities.UserRelated;
using Template_restaurant_app.Repository;

namespace Template_restaurant_app.Data.Seed
{
    public class DbSeeder
    {
        public static async Task SeedAsync(RestaurantDbContext context)
        {
            await context.Database.MigrateAsync();

            await SeedRoles(context);

            await SeedAdmin(context);

            await SeedCategories(context);
        }

        private static async Task SeedRoles(RestaurantDbContext context)
        {
            if (await context.Roles.AnyAsync())
                return;

            var roles = new List<Role>
            {
                new Role { Id = Guid.NewGuid(), Name = Roles.Admin },
                new Role { Id = Guid.NewGuid(), Name = Roles.Waiter },
                new Role { Id = Guid.NewGuid(), Name = Roles.Cashier }
            };

            await context.Roles.AddRangeAsync(roles);

            await context.SaveChangesAsync();
        }

        private static async Task SeedAdmin(RestaurantDbContext context)
        {
            if (await context.Users.AnyAsync())
                return;

            var adminRole = await context.Roles
                .FirstAsync(r => r.Name == Roles.Admin);

            var admin = new User
            {
                Id = Guid.NewGuid(),
                Username = "Admin",
                Name = "Admin",
                Email = "admin@restaurant.com"
            };

            var passwordHasher = new PasswordHasher<User>();

            admin.PasswordHash = passwordHasher.HashPassword(
                admin,
                "123456"
            );

            admin.UserRoles = new List<UserRole>
            {
                new UserRole
                {
                    UserId = admin.Id,
                    RoleId = adminRole.Id
                }
            };

            await context.Users.AddAsync(admin);

            await context.SaveChangesAsync();
        }

        private static async Task SeedCategories(RestaurantDbContext context)
        {
            var exists = await context.Categories
                .AnyAsync(c => c.Name == "Other");

            if (exists)
                return;

            await context.Categories.AddAsync(
                new Category
                {
                    Id = Guid.Parse(
                        "11111111-1111-1111-1111-111111111111"),
                    Name = "Other"
                });

            await context.SaveChangesAsync();
        }
    }
}
