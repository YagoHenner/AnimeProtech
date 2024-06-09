using Application.Features.GetAnime;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;

namespace UnitTests.Features.GetAnimeTests;

public class GetAnimeHandlerTests
{
    private readonly IAnimeRepository _animeRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<GetAnime> _validator;
    private readonly GetAnimeHandler _handler;

    public GetAnimeHandlerTests()
    {
        _animeRepository = Substitute.For<IAnimeRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<GetAnime>>();
        _handler = new GetAnimeHandler(_animeRepository, _mapper, _validator);
    }

    [Fact]
    public async Task Handle_QuandoUsarFiltrosNaRequisicao_RetornaAnimesListadosEFiltrados()
    {
        var request = new GetAnime { Nome = "One Piece", Diretor = "Eiichiro Oda" };
        _validator.ValidateAsync(request, CancellationToken.None).Returns(new ValidationResult());

        var animes = new List<Anime>
        {
            new() { Id = 1, Nome = "One Piece", Diretor = "Eiichiro Oda", Resumo = "Pirata que estica bolado" },
            new() { Id = 2, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Bebê com tatuagem na barriga vê se pode" }
        };
        _animeRepository.GetAnimes().Returns(animes);

        var getAnimeDtos = animes.Where(a => a.Nome == "One Piece" && a.Diretor == "Eiichiro Oda")
                                 .Select(a => new GetAnimeDto { Id = a.Id, Nome = a.Nome, Diretor = a.Diretor, Resumo = a.Resumo });
        _mapper.Map<IEnumerable<GetAnimeDto>>(Arg.Is<IEnumerable<Anime>>(a => a.All(x => x.Nome == "One Piece" && x.Diretor == "Eiichiro Oda")))
              .Returns(getAnimeDtos);

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeEquivalentTo(getAnimeDtos, options => options.Excluding(dto => dto.PalavrasChave));
        await _animeRepository.Received(1).GetAnimes();
    }

    [Fact]
    public async Task Handle_QuandoUsarPaginacao_RetornaAnimesPaginados()
    {
        var request = new GetAnime { Pagina = 2, QuantidadePorPagina = 1 };
        _validator.ValidateAsync(request, CancellationToken.None).Returns(new ValidationResult());

        var animes = new List<Anime>
        {
            new() { Id = 1, Nome = "One Piece", Diretor = "Eiichiro Oda", Resumo = "Pirata que estica bolado" },
            new() { Id = 2, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Bebê com tatuagem na barriga vê se pode" }
        };
        _animeRepository.GetAnimes().Returns(animes);

        var getAnimeDto = new GetAnimeDto { Id = 2, Nome = "Naruto", Diretor = "Masashi Kishimoto", Resumo = "Ninjas e amizade" };
        _mapper.Map<IEnumerable<GetAnimeDto>>(Arg.Any<IEnumerable<Anime>>()).Returns(new[] { getAnimeDto });

        var result = await _handler.Handle(request, CancellationToken.None);

        result.IsSuccess.Should().BeTrue();
        result.Value.Should().ContainSingle();
        result.Value.First().Should().BeEquivalentTo(getAnimeDto);
    }

}