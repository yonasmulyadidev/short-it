using Application.Features.Url.DTOs;
using MediatR;

namespace Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;

public sealed record GetUrlByBase64PrefixQuery(string Base64Prefix) : IRequest<UrlResponse>;