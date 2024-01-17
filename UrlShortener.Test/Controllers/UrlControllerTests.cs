using Application.Features.Url.Commands.GenerateUrl;
using Application.Features.Url.DTOs;
using Application.Features.Url.Queries.GetAllUrls;
using Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;
using Application.Features.Url.Queries.GetUrlById;
using AutoFixture;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using UrlShortenerApi.Controllers;
using Xunit;

namespace UrlShortener.Test.Controllers;

public sealed class UrlControllerTests
{
    private readonly Mock<IMediator> _mockMediator = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Given_GetAllUrlsQuery_When_GetAllUrls_IsCalled_Then_ReturnOkWithUrlResponses()
    {
        // Arrange
        var urlResponses = _fixture.CreateMany<UrlResponse>().ToList();

        var target = new UrlController(_mockMediator.Object);
        
        _mockMediator.Setup(x => x.Send(It.IsAny<GetAllUrlsQuery>(), default))
            .ReturnsAsync(urlResponses);
        
        // Act
        var response = await target.GetAllUrls();

        Assert.NotNull(response);
    }
    
    [Fact]
    public async Task Given_GetUrlByIdQuery_When_GetUrlById_IsCalled_Then_ReturnOk()
    {
        // Arrange
        var urlId = new Guid().ToString();
        var urlResponse = _fixture.Create<UrlResponse>();
        
        var target = new UrlController(_mockMediator.Object);
        
        _mockMediator.Setup(x => x.Send(It.IsAny<GetUrlByIdQuery>(), default))
            .ReturnsAsync(urlResponse);
        
        // Act
        var response = await target.GetUrlById(urlId);
        var okObjectResult = response as OkObjectResult;

        // Assert
        Assert.NotNull(okObjectResult);
    }

    [Fact]
    public async Task Given_GenerateUrlCommand_When_GenerateUrl_IsCalled_Then_ReturnCreatedAtAction()
    {
        // Arrange
        _mockMediator.Setup(x => x.Send(It.IsAny<GenerateUrlCommand>(), default))
            .ReturnsAsync(It.IsAny<string>());
        
        var target = new UrlController(_mockMediator.Object);
        
        // Act
        var response = await target.GenerateUrl(It.IsAny<GenerateUrlCommand>());
        var createdAtActionResult = response as CreatedAtActionResult;

        // Assert
        Assert.NotNull(createdAtActionResult);
    }
    
    [Fact]
    public async Task Given_GetUrlByBase64PrefixQuery_When_RedirectUrl_IsCalled_Then_ReturnRedirect()
    {
        // Arrange
        var urlPrefix = new Guid().ToString();
        var urlResponse = _fixture.Create<UrlResponse>();
        
        var target = new UrlController(_mockMediator.Object);
        
        _mockMediator.Setup(x => x.Send(It.IsAny<GetUrlByBase64PrefixQuery>(), default))
            .ReturnsAsync(urlResponse);
        
        // Act
        var response = await target.RedirectUrl(urlPrefix);
        var redirectResult = response as RedirectResult;

        // Assert
        Assert.NotNull(redirectResult);
    }
}