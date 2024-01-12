using Application.Features.Url.DTOs;
using AutoMapper;

namespace Application.Features.Url.Mappers;

public class UrlDtoProfile : Profile
{
    public UrlDtoProfile()
    {
        CreateMap<Domain.Entities.Url, UrlResponse>();
    }
}