using FluentValidation;

namespace Application.Features.GetAnime;

public class GetAnimeValidator : AbstractValidator<GetAnime>
{
    public GetAnimeValidator()
    {
        // Validações condicionais para pagina e quantidade por página
        When(x => x.Pagina.HasValue, () =>
        {
            RuleFor(x => x.Pagina).GreaterThanOrEqualTo(1).WithMessage("O número da página deve ser maior ou igual a 1.");
        });

        When(x => x.QuantidadePorPagina.HasValue, () =>
        {
            RuleFor(x => x.QuantidadePorPagina).GreaterThan(0).WithMessage("A quantidade por página deve ser maior que 0.");
        });

        RuleForEach(x => x.PalavraChave).NotEmpty().WithMessage("As palavras-chave não podem ser vazias.");
    }
}
