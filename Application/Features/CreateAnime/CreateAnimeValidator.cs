using Application.Features.Create;
using FluentValidation;

namespace Application.Features.Create;

public class CreateAnimeValidator : AbstractValidator<CreateAnime>
{
    public CreateAnimeValidator()
    {
        RuleFor(x => x.Nome).NotEmpty();
        RuleFor(x => x.Diretor).NotEmpty();
        RuleFor(x => x.Resumo).NotEmpty();
        RuleFor(x => x.PalavrasChave).NotEmpty();
    }
}
