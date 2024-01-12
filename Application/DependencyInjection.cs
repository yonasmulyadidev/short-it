using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;
        
        // Register dependencies
        return services.AddAutoMapper(assembly)
                       .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly))
                       .AddValidatorsFromAssembly(assembly);
    }
}