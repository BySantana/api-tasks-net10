using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Task.Utils;

public static class MapTaskResponse
{
    public static TaskResponse Project(TaskModel t) => new(
        t.Id,
        t.Title,
        t.Description,
        t.CreatedAt,
        t.Tags.Select(tag => new TagResponse(tag.Id, tag.Title, tag.CreatedAt)));
}