using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.GetAnime;

public class GetAnime : IRequest<Result<IEnumerable<GetAnimeDto>>>
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public int? Pagina { get; set; } = 1;
    public int? QuantidadePorPagina { get; set; } = 10;
    public IEnumerable<string>? PalavraChave { get; set; }

}
