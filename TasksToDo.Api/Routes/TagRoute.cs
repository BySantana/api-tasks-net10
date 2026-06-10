using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;
using TasksToDo.Application.Services.Tag.Queries.GetAllTags;
using TasksToDo.Application.Services.Tag.Queries.GetTagById;
using TasksToDo.Domain.Models;

namespace TasksToDo.Routes;

public static class TagRoute
{
    public static void MapTagRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tag").RequireAuthorization();

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById).WithName("GetTagById");
        ;
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
        [FromServices] IValidator<CreateTagRequest> validator, [FromServices] ITagService service)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var tagModel = new TagModel(request.Title);

        await service.UpdateAsync(id, tagModel);
        return Results.NoContent();
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices] ITagService service)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}