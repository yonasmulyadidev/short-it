using Application.Contracts.Persistence;
using Application.Features.Url.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Url.Queries.GetAllUrls;

public sealed class GetAllUrlsQueryHandler : IRequestHandler<GetAllUrlsQuery, List<UrlResponse>>
{
    private readonly IMapper _mapper;
    private readonly IUrlRepository _urlRepository;

    public GetAllUrlsQueryHandler(IMapper mapper,
        IUrlRepository urlRepository)
    {
        _mapper = mapper;
        _urlRepository = urlRepository;
    }
    
    public async Task<List<UrlResponse>> Handle(GetAllUrlsQuery request, CancellationToken cancellationToken)
    {
        var urls = await _urlRepository.GetAllAsync();
        var response = _mapper.Map<List<UrlResponse>>(urls);

        return response;
    }
}