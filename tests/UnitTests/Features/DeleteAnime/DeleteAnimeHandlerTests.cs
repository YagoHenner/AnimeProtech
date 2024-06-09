using Application.Features.DeleteAnime;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace UnitTests.Features.DeleteAnimeTests;

public class DeleteAnimeHandlerTests
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<DeleteAnime> _validator;
    private readonly DeleteAnimeHandler _handler;

    public DeleteAnimeHandlerTests()
    {
        _animeRepository = Substitute.For<IAnimeRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<DeleteAnime>>();
        _handler = new DeleteAnimeHandler(_animeRepository, _mapper, _validator);
    }

    [Fact]
    public async Task Handle_QuandoIdEhVazio_RetornaUmErroDeValidacao()
    {
        var request = new DeleteAnime { Id = 0 }; // ID vazio
        _validator.ValidateAsync(request, CancellationToken.None)
            .Returns(new ValidationResult(new[] { new ValidationFailure("Id", "O ID do anime é obrigatório") }));

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "O ID do anime é obrigatório");
        await _animeRepository.DidNotReceive().DeleteAnime(Arg.Any<Anime>());
    }

    [Fact]
    public async Task Handle_QuandoIdNaoEhNumero_RetornaUmErroDeValidacao()
    {
        var request = new DeleteAnime { Id = 1 }; // ID válido como número
        _validator.ValidateAsync(request, CancellationToken.None)
            .Returns(new ValidationResult(new[] { new ValidationFailure("Id", "O valor '1' não é válido para Id.") })); // Mensagem de erro do FluentValidation para Enums

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "O valor '1' não é válido para Id.");
        await _animeRepository.DidNotReceive().DeleteAnime(Arg.Any<Anime>());
    }

    [Fact]
    public async Task Handle_QuandoNaoEnconstraOAnimeSolicitado_RetornaUmDidNotReceive()
    {
        var request = new DeleteAnime { Id = 1 };
        _validator.ValidateAsync(request, CancellationToken.None).Returns(new ValidationResult());
        _animeRepository.GetAnimeById(request.Id).Returns((Anime?)null);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "Anime não encontrado");
        await _animeRepository.DidNotReceive().DeleteAnime(Arg.Any<Anime>());
    }
}