using Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;
using AutoFixture;
using Shouldly;
using UrlShortener.Test.BaseClasses;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Validators;

public sealed class GetUrlByBase64PrefixQueryValidatorTests : TestFixture<GetUrlByBase64PrefixQueryValidator>
{
    [Fact]
    public async Task Given_InvalidParams_When_GetUrlByBase64PrefixQueryValidator_IsCalled_Then_Throw_ValidationError()
    {
        // Arrange
        var request = Fixture.Build<GetUrlByBase64PrefixQuery>()
            .With(r => r.Base64Prefix, "")
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        Assert.Single(result.Errors); 
    }
    
    [Fact]
    public async Task Given_ValidParams_When_GetUrlByBase64PrefixQueryValidator_IsCalled_Then_Success()
    {
        // Arrange
        var request = Fixture.Build<GetUrlByBase64PrefixQuery>()
            .With(r => r.Base64Prefix, "abcde")
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        result.IsValid.ShouldBeTrue(); 
    }
}