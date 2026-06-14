namespace TasksToDo.Application.DTOs.Responses;

public record TaskResponse(Guid Id, string Title, string? Description, DateTime CreatedAt, IEnumerable<TagResponse> Tags);