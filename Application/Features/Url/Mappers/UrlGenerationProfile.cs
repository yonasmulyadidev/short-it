using Application.Features.Url.Commands.GenerateUrl;
using AutoMapper;

namespace Application.Features.Url.Mappers;

public class UrlGenerationProfile : Profile
{
    public UrlGenerationProfile()
    {
        CreateMap<GenerateUrlCommand, Domain.Entities.Url>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new Guid()))
            .ForMember(dest => dest.LongUrl, opt => opt.MapFrom(src => src.OriginalUrl));
    }
}