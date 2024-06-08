using Application.Features.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnime, Anime>().ForMember(dest => dest.PalavrasChave, opt => opt.MapFrom(src => src.PalavrasChave.Select(pc => new PalavraChave { Nome = pc })));
    }
}
