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
    internal class DeliveryMethodConfigurations : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.Property(d => d.Cost).HasColumnType("decimal(8,2)");
            builder.Property(d => d.ShortName).HasColumnType("Varchar").HasMaxLength(50);
            builder.Property(d => d.Description).HasColumnType("Varchar").HasMaxLength(100);
            builder.Property(d => d.DeliveryTime).HasColumnType("Varchar").HasMaxLength(50);
        }
    }
}
