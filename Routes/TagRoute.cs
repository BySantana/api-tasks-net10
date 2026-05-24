using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Models;
using TasksToDo.Models.DTOs.Requests;
using TasksToDo.Services.Tag;

namespace TasksToDo.Routes;

public static class TagRoute
{
    public static void MapTagRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/tag");

        group.MapGet("/", GetAll);
        group.MapGet("/{id:guid}", GetById);
        group.MapPost("/", Create);
        group.MapPut("/{id:guid}", Update);
        group.MapDelete("/{id:guid}", Delete);
    }

    private static async Task<IResult> GetAll([FromServices] ITagService service)
    {
        var tags = await service.GetAllAsync();
        return Results.Ok(tags);
    }

    private static async Task<IResult> GetById([FromRoute] Guid id, [FromServices] ITagService service)
    {
        var tags = await service.GetByIdAsync(id);
        return Results.Ok(tags);
    }
    
    private static async Task<IResult> Create([FromBody] CreateTagRequest request,
        [FromServices] IValidator<CreateTagRequest> validator, [FromServices] ITagService service)
    {
        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
            return Results.ValidationProblem(validationResult.ToDictionary());
        
        var tagModel = new TagModel(request.Title);

        var tag = await service.CreateAsync(tagModel);
        return Results.Created($"/tag/{tag.Id}", tag);
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