using System.Text;
using Domain.Constants;

namespace Domain.Extensions;

public static class UrlExtensions
{
    public static string ToBase64Prefix(this Guid urlId)
    {
        return $"{Convert.ToBase64String(Encoding.UTF8.GetBytes(urlId.ToString()))[..7]}";
    }

    public static string ToFullShortUrl(this string base64Prefix)
    {
        return $"{UrlConstants.BaseShortUrl}-{base64Prefix}";
    }

    
}