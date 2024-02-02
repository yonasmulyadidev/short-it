using Application.Features.Url.Queries.GetUrlById;
using AutoFixture;
using UrlShortener.Test.BaseClasses;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Validators;

public sealed class GetUrlByIdQueryValidatorTests : TestFixture<GetUrlByIdQueryValidator>
{
    [Fact]
    public async Task Given_InvalidParams_When_GetUrlByIdQueryValidator_IsCalled_Then_Throw_ValidationError()
    {
        // Arrange
        var request = Fixture.Build<GetUrlByIdQuery>()
            .With(r => r.UrlId, "123")
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        Assert.Single(result.Errors); 
    }
    
    [Fact]
    public async Task Given_ValidParams_When_GetUrlByIdQueryValidator_IsCalled_Then_Success()
    {
        // Arrange
        var request = Fixture.Build<GetUrlByIdQuery>()
            .With(r => r.UrlId, new Guid().ToString())
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        Assert.True(result.IsValid); 
    }
}