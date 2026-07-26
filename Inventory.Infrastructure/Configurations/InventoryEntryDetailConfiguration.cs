using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Inventory.Infrastructure.Configurations;

public class InventoryEntryDetailConfiguration : IEntityTypeConfiguration<InventoryEntryDetail>
{
    public void Configure(EntityTypeBuilder<InventoryEntryDetail> builder)
    {
        builder.ToTable("InventoryEntryDetails");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Quantity).HasPrecision(18, 2);
        builder.Property(x => x.UnitCost).HasPrecision(18, 2);
        builder.Ignore(x => x.TotalAmount);
        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
        builder.Property(x => x.UserId);
        builder.HasOne(x => x.InventoryEntry)
            .WithMany(x => x.InventoryEntryDetails)
            .HasForeignKey(x => x.InventoryEntryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Product)
            .WithMany(x => x.InventoryEntryDetails)
            .HasForeignKey(x => x.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
