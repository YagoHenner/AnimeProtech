using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Application.Features.DeleteAnime;

public class DeleteAnimeHandler(IAnimeRepository animeRepository, IMapper mapper, IValidator<DeleteAnime> validator) : IRequestHandler<DeleteAnime, Result>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(DeleteAnime request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

        var anime = await _animeRepository.GetAnimeById(request.Id);
        if (anime is null) return Result.Fail("Anime não encontrado");

        await _animeRepository.DeleteAnime(anime);
        return Result.Ok();
    }
}
