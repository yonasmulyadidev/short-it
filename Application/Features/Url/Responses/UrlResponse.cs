namespace Application.Features.Url.DTOs;

public sealed class UrlResponse
{
    public Guid Id { get; set; }

    public string ShortUrl { get; set; }

    public string LongUrl { get; set; }
    
    public DateTime DateCreated { get; set; }

    public DateTime DateModified { get; set; }
}