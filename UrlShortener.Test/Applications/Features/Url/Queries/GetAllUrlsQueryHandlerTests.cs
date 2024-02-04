using Application.Contracts.Persistence;
using Application.Features.Url.DTOs;
using Application.Features.Url.Queries.GetAllUrls;
using AutoFixture;
using AutoMapper;
using Moq;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Queries;

public sealed class GetAllUrlsQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IUrlRepository> _mockRepository = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task Given_UrlRepositoryHasData_When_GetAllUrlsQueryHandler_IsCalled_Then_ReturnCorrectResponseData()
    {
        // Arrange
        var urls = _fixture.CreateMany<global::Domain.Entities.Url>().ToList().AsReadOnly();
        var urlResponses = _fixture.CreateMany<UrlResponse>().ToList();
        var request = _fixture.Create<GetAllUrlsQuery>();
        
        var target = new GetAllUrlsQueryHandler(_mockMapper.Object, _mockRepository.Object);

        _mockRepository.Setup(r => r.GetAllAsync())
            .ReturnsAsync(urls);
        _mockMapper.Setup(m => m.Map<List<UrlResponse>>(urls))
            .Returns(urlResponses);
        
        // Act
        var result = await target.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.Equal(result.Count, urls.Count);
        _mockRepository.Verify(r => r.GetAllAsync(), Times.Once);
    }
}