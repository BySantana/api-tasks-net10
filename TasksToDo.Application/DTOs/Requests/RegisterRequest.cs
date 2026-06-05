namespace TasksToDo.Application.DTOs.Requests;

public record RegisterRequest(string Email, string Password,  string ConfirmPassword);