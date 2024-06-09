using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using FluentValidation;
using MediatR;
using static Application.Features.UpdateAnime.UpdateAnimeHandler;

namespace Application.Features.UpdateAnime;

public class UpdateAnimeHandler(IAnimeRepository animeRepository, IMapper mapper, IValidator<UpdateAnime> validator) : IRequestHandler<UpdateAnimeCommand, Result>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;

    public record UpdateAnimeCommand(int Id, UpdateAnime UpdateData) : IRequest<Result>;

    public async Task<Result> Handle(UpdateAnimeCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await validator.ValidateAsync(request.UpdateData, cancellationToken);

        if (!validationResult.IsValid)
            return Result.Fail(validationResult.Errors.Select(e => e.ErrorMessage));

        var anime = await _animeRepository.GetAnimeById(request.Id);

        if (anime is null)
            return Result.Fail("Anime não encontrado");

        _mapper.Map(request.UpdateData, anime); // Mapeamento apenas dos dados de atualização

        await _animeRepository.UpdateAnime(anime);
        return Result.Ok();
    }
}
