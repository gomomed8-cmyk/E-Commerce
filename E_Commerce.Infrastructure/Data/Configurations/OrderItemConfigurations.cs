using E_Commerce.Domain.Entites.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Data.Configurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
           
            builder.Property(o => o.Price).HasColumnType("decimal(8,2)");
            builder.OwnsOne(o => o.Product, product =>
            {
                product.Property(o => o.ProductName).HasMaxLength(100);
                product.Property(o => o.PictureUrl).HasMaxLength(200);

            });
        }
    }
}
