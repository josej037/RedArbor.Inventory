using Inventory.Application.Interfaces;
using Inventory.Infrastructure.Connections;
using Inventory.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<InventoryDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("InventoryDb"));
        });
        services.AddScoped<IConnection, InventoryConn>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
