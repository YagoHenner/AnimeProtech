using Application.Features.UpdateAnime;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace UnitTests.Features.UpdateAnimeTests;

public class UpdateAnimeHandlerTests
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<UpdateAnime> _validator;
    private readonly UpdateAnimeHandler _handler;

    public UpdateAnimeHandlerTests()
    {
        _animeRepository = Substitute.For<IAnimeRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<UpdateAnime>>();
        _handler = new UpdateAnimeHandler(_animeRepository, _mapper, _validator);
    }

    [Fact]
    public async Task Handle_QuandoNaoAchaAnime_RetornaAnimeNaoEncontrado()
    {
        var animeId = 1;
        var request = new UpdateAnimeHandler.UpdateAnimeCommand(animeId, new UpdateAnime());
        _validator.ValidateAsync(request.UpdateData, CancellationToken.None).Returns(new ValidationResult());
        _animeRepository.GetAnimeById(animeId).Returns((Anime?)null); // Retorna null

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Anime não encontrado");
        await _animeRepository.DidNotReceive().UpdateAnime(Arg.Any<Anime>());
    }

    [Fact]
    public async Task Handle_QuandoARequisicaoVemVazia_RetornaErroDeValidacaoDePreenchimento()
    {
        var animeId = 1;
        var request = new UpdateAnimeHandler.UpdateAnimeCommand(animeId, new UpdateAnime());
        _validator.ValidateAsync(request.UpdateData, CancellationToken.None)
            .Returns(new ValidationResult(new[] { new ValidationFailure("x", "Pelo menos um campo deve ser preenchido para atualizar o anime.") }));

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Pelo menos um campo deve ser preenchido para atualizar o anime.");
        await _animeRepository.DidNotReceive().UpdateAnime(Arg.Any<Anime>());
    }
}