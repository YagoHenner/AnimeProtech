using Application.Features.Create;
using Application.Features.GetAnime;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnime, Anime>().ForMember(dest => dest.PalavrasChave, opt => opt.MapFrom(src => src.PalavrasChave.Select(pc => new PalavraChave { Nome = pc })));
        CreateMap<Anime, GetAnimeDto>()
    .ForMember(
        dest => dest.PalavrasChave,
        opt => opt.MapFrom(src => src.PalavrasChave.Select(pc => pc.Nome))
    );
    }
}
