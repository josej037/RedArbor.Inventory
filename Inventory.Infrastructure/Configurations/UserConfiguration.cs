using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventory.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.FullName).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Username).IsRequired().HasMaxLength(50);
        builder.Property(x => x.Password).IsRequired().HasMaxLength(500);
        builder.Property(x => x.Active).IsRequired();
        builder.Property(x => x.CreatedAt).IsRequired();
        builder.Property(x => x.UpdatedAt);
        builder.HasIndex(x => x.Username).IsUnique();
    }
}
