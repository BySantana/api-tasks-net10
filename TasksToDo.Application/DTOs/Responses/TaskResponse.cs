namespace TasksToDo.Application.DTOs.Responses;

public record TaskResponse(string Title, string? Description, DateTime CreatedAt, IEnumerable<TagResponse> Tags);