using FluentResults;
using MediatR;

namespace Application.Features.Create;

public class CreateAnime : IRequest<Result>
{
    public string Nome { get; set; } = string.Empty;
    public string Diretor { get; set; } = string.Empty;
    public string Resumo { get; set; } = string.Empty;
    public List<string> PalavrasChave { get; set; } = [];
}
