using Application.Features.Create;
using Application.Features.DeleteAnime;
using Application.Features.GetAnime;
using Application.Features.UpdateAnime;
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

        return NoContent();
    }

    [HttpGet]
    public async Task<IActionResult> GetAnimes([FromQuery] GetAnime anime)  
    {
        var result = await _mediator.Send(anime);
        if (result.IsFailed)
            return BadRequest(result.Errors);

        return Ok(result.Value);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnime([FromRoute] int id)
    {
        var deleteAnime = new DeleteAnime { Id = id };
        var result = await _mediator.Send(deleteAnime);

        if (result.IsFailed)
            return NotFound(result.Errors);
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnime([FromRoute] int id, [FromBody] UpdateAnime updateAnime)
    {
        var result = await _mediator.Send(new UpdateAnimeHandler.UpdateAnimeCommand(id, updateAnime));

        if (result.IsFailed)
            return BadRequest(result.Errors);
        return NoContent();
    }
}
