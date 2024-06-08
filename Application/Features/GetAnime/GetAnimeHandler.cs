using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.GetAnime;

public class GetAnimeHandler(IAnimeRepository animeRepository, IMapper mapper) : IRequestHandler<GetAnime, Result<List<GetAnimeDto>>>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<GetAnimeDto>>> Handle(GetAnime request, CancellationToken cancellationToken)
    {
        var animes = await _animeRepository.GetAnimes();
        
        var result = _mapper.Map<List<GetAnimeDto>>(animes);
        return Result.Ok(result);
    }
}
