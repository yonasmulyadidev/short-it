using Application.Features.Url.DTOs;
using MediatR;

namespace Application.Features.Url.Queries.GetAllUrls;

public sealed record GetAllUrlsQuery : IRequest<List<UrlResponse>>;