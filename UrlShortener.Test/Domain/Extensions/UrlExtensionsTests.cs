using System.Text;
using Domain.Constants;
using Domain.Extensions;
using Xunit;

namespace UrlShortener.Test.Domain.Extensions;

public class UrlExtensionsTests
{
    [Fact]
    public void Given_Valid_Guid_When_ToBase64Prefix_IsCalled_Then_Return_CorrectResult()
    {
        // Arrange
        var guid = new Guid();

        // Act
        var result = guid.ToBase64Prefix();
        
        // Assert
        Assert.Equal(Convert.ToBase64String(Encoding.UTF8.GetBytes(guid.ToString()))[..7], result);
    }

    [Fact]
    public void Given_Valid_Base64Prefix_When_ToFullShortUrl_IsCalled_Then_Return_CorrectResult()
    {
        // Arrange
        const string base64Prefix = "abcdefg";
        
        // Act
        var result = base64Prefix.ToFullShortUrl();
        
        // Assert
        Assert.Equal($"{UrlConstants.BaseShortUrl}-{base64Prefix}", result);
    }
}