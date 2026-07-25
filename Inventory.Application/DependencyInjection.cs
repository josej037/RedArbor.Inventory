
using Inventory.Application.Services;
using Inventory.Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
namespace Inventory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register application services here
        services.AddScoped<ICategoryService, CategoryService>();
        return services;
    }
}

