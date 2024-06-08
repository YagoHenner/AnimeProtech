using Domain.Entities;
using FluentResults;
using MediatR;

namespace Application.Features.Create;

public class CreateAnime : IRequest<Result>
{
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
    public virtual ICollection<string> PalavrasChave { get; set; } = [];
}
