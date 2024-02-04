using System.Diagnostics.CodeAnalysis;
using Application.Contracts.Persistence;
using Domain.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Persistence.Contexts;
using Persistence.Repositories;

namespace Persistence;

[ExcludeFromCodeCoverage]
public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {
        var urlShortenerDbOption = services.BuildServiceProvider()
            .GetRequiredService<IOptions<UrlShortenerDbOptions>>().Value;
        
        return services.AddUrlShortenerDb(urlShortenerDbOption)
                       .AddPersistenceHealthChecks(urlShortenerDbOption)
                       .AddRepositories();
    }

    private static IServiceCollection AddUrlShortenerDb(this IServiceCollection services, UrlShortenerDbOptions urlShortenerDbOption)
    {
        void OptionsAction(IServiceProvider sp, DbContextOptionsBuilder options)
        {
            options.UseNpgsql(urlShortenerDbOption.ConnectionString);
        }

        services.AddDbContext<UrlDbContext>(OptionsAction);

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
        services.AddScoped<IUrlRepository, UrlRepository>();

        return services;
    }
    
    private static IServiceCollection AddPersistenceHealthChecks(this IServiceCollection services, UrlShortenerDbOptions option)
    {
        services.AddHealthChecks()
                .AddNpgSql(option.ConnectionString);

        return services;
    }
}