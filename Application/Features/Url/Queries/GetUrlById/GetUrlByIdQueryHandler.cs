using Application.Contracts.Persistence;
using Application.Exceptions;
using Application.Features.Url.DTOs;
using AutoMapper;
using MediatR;

namespace Application.Features.Url.Queries.GetUrlById;

public sealed class GetUrlByIdQueryHandler : IRequestHandler<GetUrlByIdQuery, UrlResponse>
{
    private readonly IMapper _mapper;
    private readonly IUrlRepository _urlRepository;

    public GetUrlByIdQueryHandler(IMapper mapper,
        IUrlRepository repository)
    {
        _mapper = mapper;
        _urlRepository = repository;
    }

    public async Task<UrlResponse> Handle(GetUrlByIdQuery request, CancellationToken cancellationToken)
    {
        await ValidateRequestParams(request);

        var url = await _urlRepository.GetByIdAsync(Guid.Parse(request.UrlId));

        if (url is null)
        {
            throw new NotFoundException("Url not found");
        }

        var response = _mapper.Map<UrlResponse>(url);
        return response;
    }

    //Todo: move and combine this method in a base class / not using it at all if the validator is properly injected
    private static async Task ValidateRequestParams(GetUrlByIdQuery request)
    {
        var validator = new GetUrlByIdQueryValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationErrorException("Validation errors:", validationResult.Errors);
        }
    }
}