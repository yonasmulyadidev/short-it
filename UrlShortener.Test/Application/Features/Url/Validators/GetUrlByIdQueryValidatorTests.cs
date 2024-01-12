using Application.Features.Url.Queries.GetUrlById;
using AutoFixture;
using Xunit;

namespace UrlShortener.Test.Application.Features.Url.Validators;

public class GetUrlByIdQueryValidatorTests
{
    [Fact]
    public async Task GivenInvalidParams_When_GetUrlByIdQueryValidator_IsCalled_Then_Throw_ValidationError()
    {
        // Arrange
        var fixture = new Fixture();
        var request = fixture.Build<GetUrlByIdQuery>()
            .With(r => r.UrlId, "123")
            .Create();
        
        var validator = new GetUrlByIdQueryValidator();

        // Act
        var result = await validator.ValidateAsync(request);
        
        // Assert
        Assert.Single(result.Errors); 
    }
}