using FluentResults;
using MediatR;

namespace Application.Features.DeleteAnime;

public class DeleteAnime : IRequest<Result>
{
    public int Id { get; set; }
}
