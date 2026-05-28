using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Template_restaurant_app.Domain.Entities;

namespace Template_restaurant_app.Repository.Configurations
{
    public class RestaurantTableConfiguration : IEntityTypeConfiguration<RestaurantTable>
    {
        public void Configure(EntityTypeBuilder<RestaurantTable> builder)
        {
            {
                builder.HasKey(t => t.Id);

                builder.Property(t => t.Number)
                    .IsRequired();

                builder.HasIndex(t => t.Number)
                    .IsUnique();

                builder.Property(t => t.TableStatus)
                    .IsRequired();
            }
        }
    }
}
