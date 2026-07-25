using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations
{
    public class InventoryMovementConfiguration : IEntityTypeConfiguration<InventoryMovement>
    {
        public void Configure(EntityTypeBuilder<InventoryMovement> builder)
        {
            builder.ToTable("InventoryMovements");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.MovementType).HasConversion<int>().IsRequired();
            builder.Property(x => x.ReferenceId).IsRequired();
            builder.Property(x => x.Quantity).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.StockBefore).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.StockAfter).HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UserId);
            builder.HasOne(x => x.Product)
                .WithMany(x => x.InventoryMovements)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
