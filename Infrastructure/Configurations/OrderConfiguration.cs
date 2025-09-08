using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using ProductsControl.Models.Enums;
using ProductsControl.Models;

namespace ProductsControl.Infrastructure.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.DeliveryAddress).IsRequired().HasMaxLength(200);
            builder.Property(x => x.OrderTime).IsRequired();
            builder.Property(x => x.DeliveryTime).IsRequired();
            builder.Property(x => x.Comment).HasMaxLength(200);
            builder.Property(x => x.OrderPrice).IsRequired();
            builder.Property(x => x.OrderStatus).HasConversion(new EnumToStringConverter<EOrderStatus>()).IsRequired();
        }
    }
}
