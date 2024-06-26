﻿using FluentResults;
using MediatR;

namespace Application.Features.UpdateAnime;

public class UpdateAnime : IRequest<Result>
{
    public string? Nome { get; set; }
    public string? Diretor { get; set; }
    public string? Resumo { get; set; }
}
