using DotNetEnv;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Template_restaurant_app.Repository
{
    // Factory class to create the database context at design time for migrations and other EF Core tools
    // Change the null type to your actual DbContext type, e.g., MyDbContext
    public class RestaurantDbContextFactory : IDesignTimeDbContextFactory<RestaurantDbContext>
    {
        public RestaurantDbContext CreateDbContext(string[] args)
        {
            Env.Load();

            var connection = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");

            if (string.IsNullOrWhiteSpace(connection))
                throw new Exception("Connection string não encontrada.");

            var optionsBuilder = new DbContextOptionsBuilder<RestaurantDbContext>();
            optionsBuilder.UseSqlServer(connection);

            return new RestaurantDbContext(optionsBuilder.Options);
        }
    }
}
