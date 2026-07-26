using Inventory.Application.Services;
using Inventory.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Inventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IInventoryEntryService, InventoryEntryService>();
        services.AddScoped<IInventoryExitService, InventoryExitService>();
        services.AddScoped<IInventoryMovementService, InventoryMovementService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}

