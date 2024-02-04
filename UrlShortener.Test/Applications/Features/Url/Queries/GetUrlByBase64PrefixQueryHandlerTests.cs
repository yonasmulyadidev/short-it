using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Url.DTOs;
using Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;
using AutoFixture;
using AutoMapper;
using Moq;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Queries;

public sealed class GetUrlByBase64PrefixQueryHandlerTests
{
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IUrlRepository> _mockRepository = new();
    private readonly Fixture _fixture = new();
    
    [Fact]
    public async Task Given_UrlRepositoryHasData_WithMatchingUrlPrefix_When_GetUrlByBase64PrefixQueryHandler_IsCalled_Then_ReturnCorrectResponseData()
    {
        // Arrange
        var url = _fixture.Create<global::Domain.Entities.Url>();
        var urlResponse = _fixture.Create<UrlResponse>();
        var request = _fixture.Build<GetUrlByBase64PrefixQuery>()
            .With(x => x.Base64Prefix, "test1234")
            .Create();
        
        var target = new GetUrlByBase64PrefixQueryHandler(_mockMapper.Object, _mockRepository.Object);

        _mockRepository.Setup(r => r.GetByShortPrefix(It.IsAny<string>()))
            .ReturnsAsync(url);
        _mockMapper.Setup(m => m.Map<UrlResponse>(url))
            .Returns(urlResponse);
        
        // Act
        var result = await target.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.Equal(result, urlResponse);
        _mockRepository.Verify(r => r.GetByShortPrefix(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Given_UrlRepositoryHasData_WithNoMatchingUrlPrefix_When_GetUrlByBase64PrefixQueryHandler_IsCalled_Then_Throw()
    {
        // Arrange
        var url = _fixture.Create<global::Domain.Entities.Url>();
        var urlResponse = _fixture.Create<UrlResponse>();
        var request = _fixture.Build<GetUrlByBase64PrefixQuery>()
            .With(x => x.Base64Prefix, "test1234")
            .Create();

        var target = new GetUrlByBase64PrefixQueryHandler(_mockMapper.Object, _mockRepository.Object);

        _mockRepository.Setup(r => r.GetByShortPrefix(It.IsAny<string>()))
            .ReturnsAsync((global::Domain.Entities.Url?)null);
        _mockMapper.Setup(m => m.Map<UrlResponse>(url))
            .Returns(urlResponse);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(async () =>
            await target.Handle(request, CancellationToken.None)
        );
    }
    
    [Fact]
    public async Task Given_InvalidQuery_When_GetUrlByBase64PrefixQueryHandler_IsCalled_Then_Throw()
    {
        // Arrange
        const string invalidBasePrefix = "";
        var request = _fixture.Build<GetUrlByBase64PrefixQuery>()
            .With(x => x.Base64Prefix, invalidBasePrefix)
            .Create();
        
        var target = new GetUrlByBase64PrefixQueryHandler(_mockMapper.Object, _mockRepository.Object);
        
        // Act & Assert
        await Assert.ThrowsAsync<ValidationErrorException>(async () => 
            await target.Handle(request, CancellationToken.None)
        );
    }
}