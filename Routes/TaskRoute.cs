using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Models;
using TasksToDo.Models.DTOs.Requests;
using TasksToDo.Services.Task;

namespace TasksToDo.Routes;

public static class TaskRoute
{
    public static void MapTaskRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/task");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);
    }

    private static async Task<IResult> GetAll([FromServices] ITaskService service)
    {
        var tasks = await service.GetAllAsync();
        return Results.Ok(tasks);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, [FromServices] ITaskService service)
    {
        var task = await service.GetByIdAsync(id);
        return task == null
            ? Results.NotFound()
            : Results.Ok(task);
    }

    private static async Task<IResult> Create([FromBody] CreateTaskRequest request,
        [FromServices] IValidator<CreateTaskRequest> validator, [FromServices] ITaskService service)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());

        var taskDto = new TaskModel(
            request.Title,
            request.TagId,
            request.Description);

        var task = await service.CreateAsync(taskDto);
        return Results.Created($"/task/Id", task);
    }

    private static async Task<IResult> Update([FromRoute] Guid id, [FromBody] CreateTaskRequest request,
        [FromServices] IValidator<CreateTaskRequest> validator, [FromServices] ITaskService service)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());
        
        var taskDto = new TaskModel(
            request.Title,
            request.TagId,
            request.Description);

        await service.UpdateAsync(id, taskDto);
        return Results.NoContent();
    }

    private static async Task<IResult> Delete([FromRoute] Guid id, [FromServices] ITaskService service)
    {
        var deleted = await service.DeleteAsync(id);
        return deleted ? Results.NoContent() : Results.NotFound();
    }
}