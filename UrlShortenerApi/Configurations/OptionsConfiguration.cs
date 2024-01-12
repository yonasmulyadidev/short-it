using Domain.Configurations;

namespace UrlShortenerApi.Configurations;

public static class OptionsConfiguration
{
    public static IServiceCollection AddOptionsConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<UrlShortenerDbOptions>()
            .Bind(configuration.GetSection(nameof(UrlShortenerDbOptions)));

        return services;
    }
}
