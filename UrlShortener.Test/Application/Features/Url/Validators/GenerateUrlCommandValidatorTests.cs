using Application.Features.Url.Commands.GenerateUrl;
using AutoFixture;
using Xunit;

namespace UrlShortener.Test.Application.Features.Url.Validators;

public class GenerateUrlCommandValidatorTests
{
    [Fact]
    public async Task GivenInvalidParams_When_GenerateUrlCommandValidator_IsCalled_Then_Throw_ValidationError()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Build<GenerateUrlCommand>()
            .With(r => r.OriginalUrl, "")
            .Create();
        
        var validator = new GenerateUrlCommandValidator();

        // Act
        var result = await validator.ValidateAsync(request);
        
        // Assert
        Assert.Single(result.Errors); 
    }
}