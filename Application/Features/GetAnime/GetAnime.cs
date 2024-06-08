using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.GetAnime;

public class GetAnime : IRequest<Result<List<GetAnimeDto>>>
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public string? PalavraChave { get; set; }
}
