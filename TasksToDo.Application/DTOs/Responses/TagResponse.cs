namespace TasksToDo.Application.DTOs.Responses;

public record TagResponse(Guid Id, string Title, DateTime CreatedAt);