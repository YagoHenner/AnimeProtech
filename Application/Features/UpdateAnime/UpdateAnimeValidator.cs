using FluentValidation;

namespace Application.Features.UpdateAnime;

public class UpdateAnimeValidator : AbstractValidator<UpdateAnime>
{
    public UpdateAnimeValidator()
    {
        RuleFor(x => x).Must(x => x.Nome != null || x.Diretor != null || x.Resumo != null)
                       .WithMessage("Pelo menos um campo deve ser preenchido para atualizar o anime.");
    }
}