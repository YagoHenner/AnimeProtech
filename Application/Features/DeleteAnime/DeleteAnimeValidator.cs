using FluentValidation;

namespace Application.Features.DeleteAnime;

public class DeleteAnimeValidator : AbstractValidator<DeleteAnime>
{
    public DeleteAnimeValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        RuleFor(x => x.Id).IsInEnum();
    }
}
