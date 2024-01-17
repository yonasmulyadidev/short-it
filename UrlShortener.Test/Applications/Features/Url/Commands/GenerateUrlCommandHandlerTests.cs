using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Url.Commands.GenerateUrl;
using AutoFixture;
using AutoMapper;
using Moq;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Commands;

public class GenerateUrlCommandHandlerTests
{
    private readonly Mock<IMapper> _mockMapper = new();
    private readonly Mock<IUrlRepository> _mockRepository = new();
    private readonly Fixture _fixture = new();
    
    [Fact]
    public async Task Given_Valid_Request_And_NoDuplicate_When_GenerateUrlCommandHandler_IsCalled_Then_Succeed()
    {
        // Arrange
        var url = _fixture.Build<Domain.Entities.Url>()
            .With(x => x.ShortUrl, "abcde")
            .Create();
        var request = _fixture.Build<GenerateUrlCommand>()
            .With(x => x.OriginalUrl, "www.test1234.com")
            .Create();
        
        var target = new GenerateUrlCommandHandler(_mockRepository.Object, _mockMapper.Object);

        
        _mockRepository.Setup(r => r.GetByOriginalUrl(It.IsAny<string>()))
            .ReturnsAsync((Domain.Entities.Url?)null);
        _mockRepository.Setup(r => r.CreateAsync(It.IsAny<Domain.Entities.Url>()))
            .ReturnsAsync(url);
        _mockMapper.Setup(m => m.Map<Domain.Entities.Url>(It.IsAny<GenerateUrlCommand>()))
        .Returns(url);
        
        // Act
        var response = await target.Handle(request, CancellationToken.None);
        
        // Assert
        Assert.NotEmpty(response);
        _mockRepository.Verify(r => r.GetByOriginalUrl(It.IsAny<string>()), Times.Once);
        _mockRepository.Verify(r => r.CreateAsync(It.IsAny<Domain.Entities.Url>()), Times.Once);
    }
    
    [Fact]
    public async Task Given_Valid_Request_And_HaveDuplicateData_When_GenerateUrlCommandHandler_IsCalled_Then_Throw()
    {
        // Arrange
        var url = _fixture.Build<Domain.Entities.Url>()
            .With(x => x.ShortUrl, "abcde")
            .Create();
        var request = _fixture.Build<GenerateUrlCommand>()
            .With(x => x.OriginalUrl, "www.test1234.com")
            .Create();
        
        var target = new GenerateUrlCommandHandler(_mockRepository.Object, _mockMapper.Object);

        
        _mockRepository.Setup(r => r.GetByOriginalUrl(It.IsAny<string>()))
            .ReturnsAsync(url);
        
        // Act & Assert
        await Assert.ThrowsAsync<DuplicateException>(async () => 
            await target.Handle(request, CancellationToken.None)
        );
    }
}