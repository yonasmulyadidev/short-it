using Domain.Entities;
using Persistence.Contexts;
using UrlShortener.IntegrationTest.Constants;

namespace UrlShortener.IntegrationTest.BaseClasses;

public static class SeedData
{
    public static void Initialize(UrlDbContext context)
    {
        if (!context.Urls.Any())
        {
            context.Urls.AddRange(
                new Url
                {
                    Id = TestConstants.UrlId1,
                    ShortUrl = TestConstants.UrlShortPrefix1,
                    LongUrl = TestConstants.OriginalUrl1,
                }
            );

            context.SaveChanges();
        }
    }
}
