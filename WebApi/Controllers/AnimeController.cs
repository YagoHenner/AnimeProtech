using Application.Features.Create;
using Application.Features.GetAnime;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnimeController : ControllerBase
{
    private readonly IMediator _mediator;

    public AnimeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAnime anime)
    {
        var result = await _mediator.Send(anime);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Successes);
    }

    [HttpPost("get")]
    public async Task<IActionResult> GetAnimes(GetAnime anime)  
    {
        var result = await _mediator.Send(anime);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }
}
