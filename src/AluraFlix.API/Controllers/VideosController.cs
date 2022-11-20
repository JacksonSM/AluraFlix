﻿using AluraFlix.API.Tools;
using AluraFlix.Application.UseCases.Commands;
using AluraFlix.Application.UseCases.Commands.Video;
using AluraFlix.Application.UseCases.Handlers.Video;
using AluraFlix.Application.UseCases.Handlers.Video.AdicionarVideo;
using AluraFlix.Application.UseCases.Handlers.Video.AtualizarVideo;
using AluraFlix.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AluraFlix.API.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = ("ADMIN,USER"))]
public class VideosController : ControllerBase
{
    [HttpPost]
    [Authorize("ADMIN")]
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(Video))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Add(
    [FromBody] AdicionarVideoCommand command,
    [FromServices] AdicionarVideoHandler handler)
    {
        return new ParseRequestResult().ParseToActionResult(await handler.Handle(command));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetAll(
    [FromQuery] ObterTodosVideosCommand command,
    [FromServices] ObterTodosVideosHandler handler)
    {
        return new ParseRequestResult().ParseToActionResult(await handler.Handle(command), Response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> GetById(
    int id,
    [FromServices] ObterVideoPorIdHandler handler)
    {
        return new ParseRequestResult().ParseToActionResult(await handler.Handle(new GetByIdCommand { Id = id}));
    }

    [HttpPut("{id}")]
    [Authorize("ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> Update(
    int id,
    [FromBody] AtualizarVideoCommand command,
    [FromServices] AtualizarVideoHandler handler)
    {
        command.SetId(id);
        return new ParseRequestResult().ParseToActionResult(await handler.Handle(command));
    }

    [HttpDelete("{id}")]
    [Authorize("ADMIN")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
    public async Task<ActionResult> Delete(
    int id,
    [FromServices] DeletarVideoHandler handler)
    {
        return new ParseRequestResult()
            .ParseToActionResult(await handler.Handle(new DeletarVideoCommand { Id = id}));
    }

    [HttpGet("query")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<ActionResult> GetByQuery(
    [FromQuery] GetByQueryCommand command,
    [FromServices] ObterVideoPorQueryHandler handler)
    {
        return new ParseRequestResult().ParseToActionResult(await handler.Handle(command), Response);
    }
}
