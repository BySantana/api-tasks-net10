using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Tag.Utils;

public static class MapResponse
{
    public static TagResponse Project(TagModel t) => new(
        t.Id,
        t.Title,
        t.CreatedAt);
}