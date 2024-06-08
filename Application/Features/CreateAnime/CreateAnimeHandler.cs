using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Features.Create;

public class CreateAnimeHandler(IAnimeRepository animeRepository, IMapper mapper, IValidator<CreateAnime> validator) : IRequestHandler<CreateAnime, Result>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result> Handle(CreateAnime request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

        var anime = _mapper.Map<Anime>(request);

        await _animeRepository.AddAsync(anime);
        return Result.Ok();
    }
}
