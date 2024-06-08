using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.GetAnime;

public class GetAnime : IRequest<Result<IEnumerable<GetAnimeDto>>>
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public IEnumerable<string>? PalavraChave { get; set; }
}
