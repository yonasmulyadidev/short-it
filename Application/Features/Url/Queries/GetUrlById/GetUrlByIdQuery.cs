using Application.Features.Url.DTOs;
using MediatR;

namespace Application.Features.Url.Queries.GetUrlById;

public sealed record GetUrlByIdQuery(string UrlId) : IRequest<UrlResponse>;