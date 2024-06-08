using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.GetAnime;

public class GetAnimeHandler(IAnimeRepository animeRepository, IMapper mapper) : IRequestHandler<GetAnime, Result<IEnumerable<GetAnimeDto>>>
{
    private readonly IAnimeRepository _animeRepository = animeRepository;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<IEnumerable<GetAnimeDto>>> Handle(GetAnime request, CancellationToken cancellationToken)
    {
        var animes = await _animeRepository.GetAnimes();

        if (!String.IsNullOrEmpty(request.Nome))
        {
            animes = animes.Where(a => a.Nome.Contains(request.Nome));
        }

        if (!String.IsNullOrEmpty(request.Diretor))
        {
            animes = animes.Where(a => a.Diretor.Contains(request.Diretor));
        }
        
        if (request.PalavraChave is not null && request.PalavraChave.Count() != 0)
        {
            animes = animes.Where(a => request.PalavraChave.Any(pc => a.Resumo.Contains(pc)));
        }

        var result = _mapper.Map<IEnumerable<GetAnimeDto>>(animes);
        return Result.Ok(result);
    }
}
