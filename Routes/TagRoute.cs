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
        group.MapPost("/", Create);
    }
    
    private static async Task<IResult> GetAll([FromServices] ITagService service)
    {
        var tags = await service.GetAllAsync();
        return Results.Ok(tags);
    }
    
    private static async Task<IResult> Create([FromBody] CreateTagRequest request, [FromServices] ITagService service)
    {
        var tagModel = new TagModel(request.Title);
        
        var tag = await service.CreateAsync(tagModel);
        return Results.Created($"/tag/{tag.Id}", tag);
    }
}