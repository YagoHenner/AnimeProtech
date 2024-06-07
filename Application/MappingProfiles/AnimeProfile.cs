using Application.Features.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnime, Anime>();
    }
}
