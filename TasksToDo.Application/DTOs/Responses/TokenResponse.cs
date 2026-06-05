namespace TasksToDo.Application.DTOs.Responses;

public record TokenResponse(string Token, DateTime ExpiresAt);