using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations
{
    public class InventoryExitConfiguration : IEntityTypeConfiguration<InventoryExit>
    {
        public void Configure(EntityTypeBuilder<InventoryExit> builder)
        {
            builder.ToTable("InventoryExits");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Client).IsRequired().HasMaxLength(150);
            builder.Property(x => x.OrderNumber).IsRequired().HasMaxLength(50);
            builder.Ignore(x => x.DeliveredDate);
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UserId);
            builder.HasMany(x => x.InventoryExitDetails)
                .WithOne(x => x.InventoryExit)
                .HasForeignKey(x => x.InventoryExitId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
