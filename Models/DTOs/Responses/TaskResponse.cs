namespace TasksToDo.Models.DTOs.Responses;

public record TaskResponse(string Title, string? Description, DateTime CreatedAt, IEnumerable<TagResponse> Tags);