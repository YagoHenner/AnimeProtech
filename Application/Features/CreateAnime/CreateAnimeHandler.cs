using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.Create;

public class CreateAnimeHandler(IAnimeRepository animeRepository, IMapper mapper) : IRequestHandler<CreateAnime, Result>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(CreateAnime request, CancellationToken cancellationToken)
    {
        var anime = _mapper.Map<Anime>(request);

        await _animeRepository.AddAsync(anime);
        return Result.Ok();
    }
}
