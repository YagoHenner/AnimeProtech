using Application.Features.Create;
using Application.Features.GetAnime;
using Application.Features.UpdateAnime;
using AutoMapper;
using Domain.Entities;

namespace Application.MappingProfiles;

public class AnimeProfile : Profile
{
    public AnimeProfile()
    {
        CreateMap<CreateAnime, Anime>();
        CreateMap<Anime, GetAnimeDto>();
        CreateMap<UpdateAnime, Anime>()
     .ForAllMembers(opts => opts.Condition(
         (src, dest, sourceMember) => sourceMember != null));
    }
}
