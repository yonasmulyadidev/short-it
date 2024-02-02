using System.Net;
using System.Net.Http.Json;
using Application.Features.Url.Commands.GenerateUrl;
using UrlShortener.IntegrationTest.BaseClasses;
using UrlShortener.IntegrationTest.Constants;
using Xunit;

namespace UrlShortener.IntegrationTest.Controllers;

public class UrlControllerIntegrationTests : IClassFixture<IntegrationTestFactory<Program>>
{
    private readonly HttpClient _client;

    public UrlControllerIntegrationTests(IntegrationTestFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task When_GetAllUrls_Endpoint_Is_Triggered_Then_Return_Ok()
    {
        // Act
        var response = await _client.GetAsync("/api/Url");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task When_GetUrlById_Endpoint_Is_Triggered_Then_Return_Ok()
    {
        // Arrange
        var urlId = TestConstants.UrlId1.ToString();

        // Act
        var response = await _client.GetAsync($"/api/Url/{urlId}");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]
    public async Task When_GenerateUrl_Endpoint_Is_Called_Then_Return_Created()
    {
        // Act
        var response = await _client.PostAsJsonAsync("/api/Url", new GenerateUrlCommand
        {
            OriginalUrl = TestConstants.OriginalUrl1
        });

        // Assert
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
    }
}