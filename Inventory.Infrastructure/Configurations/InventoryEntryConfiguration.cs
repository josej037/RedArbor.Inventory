using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations
{
    internal class InventoryEntryConfiguration : IEntityTypeConfiguration<InventoryEntry>
    {
        public void Configure(EntityTypeBuilder<InventoryEntry> builder)
        {
            builder.ToTable("InventoryEntries");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Supplier).IsRequired().HasMaxLength(150);
            builder.Property(x => x.InvoiceNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.ReceivedDate).IsRequired();
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.Property(x => x.UserId);
            builder.HasMany(x => x.InventoryEntryDetails)
                .WithOne(x => x.InventoryEntry)
                .HasForeignKey(x => x.InventoryEntryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
