using Application.Features.Create;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace UnitTests.Features.CreateAnimeTests;

public class CreateAnimeHandlerTests
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateAnime> _validator;
    private readonly CreateAnimeHandler _handler;

    public CreateAnimeHandlerTests()
    {
        _animeRepository = Substitute.For<IAnimeRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<CreateAnime>>();
        _handler = new CreateAnimeHandler(_animeRepository, _mapper, _validator);
    }

    [Fact]
    public async Task Handle_QuandoReceberUmAnime_CriaUmAnime()
    {
        var request = new CreateAnime { Nome = "Attack on Titan", Diretor = "Tetsurō Araki", Resumo = "Porradaria entre humanos e titãs" };
        _validator.ValidateAsync(request, CancellationToken.None).Returns(new ValidationResult());

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        await _animeRepository.Received(1).AddAsync(Arg.Any<Anime>());
    }

    [Fact]
    public async Task Handle_QuandoReceberAnimeComParâmetrosVazios_RetornaFalhaNaValidacao()
    {
        var request = new CreateAnime { Nome = "", Diretor = "", Resumo = "" };
        _validator.ValidateAsync(request, CancellationToken.None)
            .Returns(new ValidationResult(new[] { new ValidationFailure("Nome", "O nome do anime é obrigatório") }));
        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsFailed.Should().BeTrue();
        result.Errors.Should().ContainSingle(e => e.Message == "O nome do anime é obrigatório");
        await _animeRepository.DidNotReceive().AddAsync(Arg.Any<Anime>());
    }
}