using Inventory.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Inventory.Infrastructure.Seed;

public static class DataSeed
{
    public static async Task Seed(InventoryDbContext context)
    {
        for (int i = 0; i < 5; i++)
        {
            try
            {
                await context.Database.MigrateAsync();
                break;
            }
            catch
            {
                if (i == 4)
                    throw;

                await Task.Delay(5000);
            }
        }

        if (!await context.Users.AnyAsync())
        {
            context.Users.Add(new User
            {
                Username = "admin",
                Password = "Admin123*",
                FullName = "Administrator",
                Active = true,
                CreatedAt = DateTime.UtcNow
            });
            await context.SaveChangesAsync();
        }

        if (!await context.Categories.AnyAsync())
        {
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Electronics",
                    Description = "Electronic products",
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Category
                {
                    Name = "Office",
                    Description = "Office supplies",
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                },
            };

            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();
        }

        if (!await context.Products.AnyAsync())
        {
            var electronics = await context.Categories.FirstAsync(c => c.Name == "Electronics");
            var office = await context.Categories.FirstAsync(c => c.Name == "Office");
            var products = new List<Product>
            {
                new Product
                {
                    Name = "Laptop",
                    Description = "15 inch laptop",
                    Price = 1200,
                    Stock = 10,
                    CategoryId = electronics.Id,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Mouse",
                    Description = "Wireless mouse",
                    Price = 25,
                    Stock = 30,
                    CategoryId = electronics.Id,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Pen",
                    Description = "Blue ink pen",
                    Price = 2,
                    Stock = 200,
                    CategoryId = office.Id,
                    Active = true,
                    CreatedAt = DateTime.UtcNow
                }
            };

            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }

    }
}
