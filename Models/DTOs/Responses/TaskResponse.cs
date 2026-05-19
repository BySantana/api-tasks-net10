namespace TasksToDo.Models.DTOs.Responses;

public record TaskResponse(string Title, string? Description, TagResponse? Tag);