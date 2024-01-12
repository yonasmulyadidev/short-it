using MediatR;

namespace Application.Features.Url.Commands.GenerateUrl;

public sealed class GenerateUrlCommand : IRequest<string>
{
    public string OriginalUrl { get; set; }
}