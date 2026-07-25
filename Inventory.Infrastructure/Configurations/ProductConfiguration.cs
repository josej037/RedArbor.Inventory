using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Description).HasMaxLength(500);
            builder.Property(x => x.Price).HasPrecision(18, 2);
            builder.Property(x => x.Stock).HasPrecision(18, 2);
            builder.Property(x => x.Active).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();
            builder.Property(x => x.UpdatedAt);
            builder.HasIndex(x => x.Name);
            builder.HasIndex(x => x.CategoryId);
            builder.HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.InventoryEntryDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.InventoryExitDetails)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(x => x.InventoryMovements)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
