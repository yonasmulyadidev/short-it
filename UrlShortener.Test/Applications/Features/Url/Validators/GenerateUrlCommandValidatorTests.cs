using Application.Features.Url.Commands.GenerateUrl;
using AutoFixture;
using Shouldly;
using UrlShortener.Test.BaseClasses;
using Xunit;

namespace UrlShortener.Test.Applications.Features.Url.Validators;

public sealed class GenerateUrlCommandValidatorTests : TestFixture<GenerateUrlCommandValidator>
{
    [Fact]
    public async Task GivenInvalidParams_When_GenerateUrlCommandValidator_IsCalled_Then_Throw_ValidationError()
    {
        // Arrange
        var request = Fixture.Build<GenerateUrlCommand>()
            .With(r => r.OriginalUrl, "")
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        Assert.Single(result.Errors); 
    }
    
    [Fact]
    public async Task GivenValidParams_When_GenerateUrlCommandValidator_IsCalled_Then_Success()
    {
        // Arrange
        var request = Fixture.Build<GenerateUrlCommand>()
            .With(r => r.OriginalUrl, "www.test1234.com")
            .Create();

        // Act
        var result = await Target.ValidateAsync(request);
        
        // Assert
        result.IsValid.ShouldBeTrue();
    }
}