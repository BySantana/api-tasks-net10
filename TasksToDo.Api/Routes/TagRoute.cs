using MediatR;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;
using TasksToDo.Application.Services.Tag.Commands.DeleteTag;
using TasksToDo.Application.Services.Tag.Commands.UpdateTag;
using TasksToDo.Application.Services.Tag.Queries.GetAllTags;
using TasksToDo.Application.Services.Tag.Queries.GetTagById;

namespace TasksToDo.Routes;

public static class TagRoute
{
    public static void MapTagRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tag"); //.RequireAuthorization();

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById).WithName("GetTagById");
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);
    }

    private static async Task<IResult> GetAll([AsParameters] PagedRequest request, [FromServices] ISender sender,
        CancellationToken ct)
    {
        var req = new GetAllTagsQuery(request);
        var tags = await sender.Send(req, ct);
        return Results.Ok(tags);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, [FromServices] ISender sender, CancellationToken ct)
    {
        var req = new GetTagByIdQuery(id);
        var tags = await sender.Send(req, ct);
        return Results.Ok(tags);
    }

    private static async Task<IResult> Create([FromBody] CreateTagCommand command,
        [FromServices] ISender sender, CancellationToken ct)
    {
        var id = await sender.Send(command, ct);
        return Results.CreatedAtRoute("GetTagById", new { id });
    }

    private static async Task<IResult> Update([FromRoute] Guid id, [FromBody] CreateTagRequest request,
        [FromServices] ISender sender, CancellationToken ct)
    {
        var command = new UpdateTagCommand(id, request.Title);
        await sender.Send(command, ct);
        return Results.NoContent();
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices] ISender sender, CancellationToken ct)
    {
        var request = new DeleteTagCommand(id);
        var deleted = await sender.Send(request, ct);

        return deleted ? Results.NoContent() : Results.NotFound();
    }
}