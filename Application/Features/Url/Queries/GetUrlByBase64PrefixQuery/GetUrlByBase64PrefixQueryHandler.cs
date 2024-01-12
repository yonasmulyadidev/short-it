using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Url.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Url.Queries.GetUrlByBase64PrefixQuery;

public class GetUrlByBase64PrefixQueryHandler : IRequestHandler<GetUrlByBase64PrefixQuery, UrlResponse>
{
    private readonly IMapper _mapper;
    private readonly IUrlRepository _urlRepository;
    
    public GetUrlByBase64PrefixQueryHandler(IMapper mapper,
        IUrlRepository repository)
    {
        _mapper = mapper;
        _urlRepository = repository;
    }

    public async Task<UrlResponse> Handle(GetUrlByBase64PrefixQuery request, CancellationToken cancellationToken)
    {
        await ValidateRequestParams(request);

        var url = await _urlRepository.GetByShortPrefix(request.Base64Prefix);

        if (url is null)
        {
            throw new NotFoundException("Url not found");
        }

        var response = _mapper.Map<UrlResponse>(url);
        return response;
    }
    
    //Todo: move and combine this method in a base class / not using it at all if the validator is properly injected
    private static async Task ValidateRequestParams(GetUrlByBase64PrefixQuery request)
    {
        var validator = new GetUrlByBase64PrefixQueryValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationErrorException("Validation errors:", validationResult.Errors);
        }
    }
}