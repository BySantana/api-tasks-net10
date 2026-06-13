using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Application.Services.Task.Commands.CreateTask;
using TasksToDo.Application.Services.Task.Commands.DeleteTask;
using TasksToDo.Application.Services.Task.Commands.UpdateTask;
using TasksToDo.Domain.Models;

namespace TasksToDo.Routes;

public static class TaskRoute
{
    public static void MapTaskRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/task");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById).WithName("GetTaskById");
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);
        group.MapPut("/Tag/Add/{taskId:guid}&{tagId:guid}", AddTag);
        group.MapPut("/Tag/Remove/{taskId:guid}&{tagId:guid}", RemoveTag);
    }

    private static async Task<IResult> GetAll([AsParameters] PagedRequest request, [FromServices] ITaskService service)
    {
        var tasks = await service.GetAllAsync(request);
        return Results.Ok(tasks);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, [FromServices] ITaskService service)
    {
        var task = await service.GetByIdAsync(id);
        return task == null
            ? Results.NotFound()
            : Results.Ok(task);
    }

    private static async Task<IResult> Create([FromBody] CreateTaskCommand command,
        [FromServices] ISender sender, CancellationToken ct)
    {
        var request = new CreateTaskCommand(command.Title, command.Description);

        var id = await sender.Send(request, ct);
        return Results.CreatedAtRoute("GetTaskById", new { id });
    }

    private static async Task<IResult> Update([FromRoute] Guid id, [FromBody] UpdateTaskCommand command,
        [FromServices] ISender sender, CancellationToken ct)
    {
        var request = new UpdateTaskCommand(command.Id, command.Title, command.Description);

        await sender.Send(request, ct);
        return Results.NoContent();
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices] ISender sender, CancellationToken ct)
    {
        var request = new DeleteTaskCommand(id);
        var deleted = await sender.Send(request, ct);
        return deleted ? Results.NoContent() : Results.NotFound();
    }

    private static async Task<IResult> RemoveTag([FromRoute] Guid taskId, [FromRoute] Guid tagId,
        [FromServices] ITaskService service)
    {
        var task = await service.RemoveTagAsync(taskId, tagId);
        return Results.Ok(task);
    }

    private static async Task<IResult> AddTag([FromRoute] Guid taskId, [FromRoute] Guid tagId,
        [FromServices] ITaskService service)
    {
        var task = await service.AddTagAsync(taskId, tagId);
        return Results.Ok(task);
    }
}