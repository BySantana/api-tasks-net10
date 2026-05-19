namespace TasksToDo.Models.DTOs.Requests;

public record CreateTaskRequest(string Title, Guid TagId, string Description);