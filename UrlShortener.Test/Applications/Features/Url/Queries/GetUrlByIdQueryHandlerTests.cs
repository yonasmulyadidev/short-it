using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Url.DTOs;
using Application.Features.Url.Queries.GetUrlById;
using AutoFixture;
using AutoMapper;
using Moq;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Queries;

public sealed class GetUrlByIdQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IUrlRepository> _mockRepository = new();
    private readonly Fixture _fixture = new();
    
    [Fact]
    public async Task Given_UrlRepositoryHasData_WithMatchingId_When_GetUrlByIdQueryHandler_IsCalled_Then_ReturnCorrectResponseData()
    {
        // Arrange
        var url = _fixture.Create<Domain.Entities.Url>();
        var urlResponse = _fixture.Create<UrlResponse>();
        var request = _fixture.Build<GetUrlByIdQuery>()
            .With(x => x.UrlId, new Guid().ToString())
            .Create();
        
        var target = new GetUrlByIdQueryHandler(_mockMapper.Object, _mockRepository.Object);

        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))
            .ReturnsAsync(url);
        _mockMapper.Setup(m => m.Map<UrlResponse>(url))
            .Returns(urlResponse);
        
        // Act
        var result = await target.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.Equal(result, urlResponse);
        _mockRepository.Verify(r => r.GetByIdAsync(It.IsAny<Guid>()), Times.Once);
    }
    
    [Fact]
    public async Task Given_UrlRepositoryHasData_WithNoMatchingId_When_GetUrlByIdQueryHandler_IsCalled_Then_Throw()
    {
        // Arrange
        var url = _fixture.Create<Domain.Entities.Url>();
        var urlResponse = _fixture.Create<UrlResponse>();
        var request = _fixture.Build<GetUrlByIdQuery>()
            .With(x => x.UrlId, new Guid().ToString())
            .Create();
        
        var target = new GetUrlByIdQueryHandler(_mockMapper.Object, _mockRepository.Object);

        _mockRepository.Setup(r => r.GetByIdAsync(It.IsAny<Guid>()))!
            .ReturnsAsync((Domain.Entities.Url)null!);
        _mockMapper.Setup(m => m.Map<UrlResponse>(url))
            .Returns(urlResponse);
        
        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () => 
            await target.Handle(request, CancellationToken.None)
        );
    }
}