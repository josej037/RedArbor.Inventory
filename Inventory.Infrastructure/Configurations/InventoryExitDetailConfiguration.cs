using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations
{
    public class InventoryExitDetailConfiguration : IEntityTypeConfiguration<InventoryExitDetail>
    {
        public void Configure(EntityTypeBuilder<InventoryExitDetail> builder)
        {
            builder.ToTable("InventoryExitDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Quantity).HasPrecision(18, 2);
            builder.Property(x => x.UnitCost).HasPrecision(18, 2);
            builder.Ignore(x => x.TotalAmount);
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UserId);
            builder.HasOne(x => x.InventoryExit)
                .WithMany(x => x.InventoryExitDetails)
                .HasForeignKey(x => x.InventoryExitId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Product)
                .WithMany(x => x.InventoryExitDetails)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
