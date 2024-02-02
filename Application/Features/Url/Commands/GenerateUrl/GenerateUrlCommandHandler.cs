using Application.Contracts.Persistence;
using Application.Exceptions;
using AutoMapper;
using Domain.Extensions;
using MediatR;

namespace Application.Features.Url.Commands.GenerateUrl;

public sealed class GenerateUrlCommandHandler : IRequestHandler<GenerateUrlCommand, string>
{
    private readonly IUrlRepository _repository;
    private readonly IMapper _mapper;

    public GenerateUrlCommandHandler(IUrlRepository repository
        , IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<string> Handle(GenerateUrlCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequestParams(request);
        
        var originalUrl = await _repository.GetByOriginalUrl(request.OriginalUrl);

        if (originalUrl != null)
        {
            throw new DuplicateException("Duplicate entry found for the original URL");
        }

        var newUrl = _mapper.Map<Domain.Entities.Url>(request);
        newUrl.ShortUrl = newUrl.Id.ToBase64Prefix();

        var response = await _repository.CreateAsync(newUrl);

        return response.ShortUrl.ToFullShortUrl();
    }
    
    //Todo: move and combine this method in a base class / not using it at all if the validator is properly injected
    private static async Task ValidateRequestParams(GenerateUrlCommand request)
    {
        var validator = new GenerateUrlCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationErrorException("Validation errors:", validationResult.Errors);
        }
    }
}