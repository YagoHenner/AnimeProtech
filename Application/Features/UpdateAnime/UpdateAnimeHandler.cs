using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.UpdateAnime;

public class UpdateAnimeHandler(IAnimeRepository animeRepository, IMapper mapper) : IRequestHandler<UpdateAnime, Result>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(UpdateAnime request, CancellationToken cancellationToken)
    {
        var anime = await _animeRepository.GetAnimeById(request.Id);

        if (anime is null)
            return Result.Fail("Anime não encontrado");

        _mapper.Map(request, anime);

        await _animeRepository.UpdateAnime(anime);
        return Result.Ok();
    }
}

