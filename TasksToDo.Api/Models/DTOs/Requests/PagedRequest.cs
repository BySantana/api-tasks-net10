namespace TasksToDo.Models.DTOs.Requests;

public record PagedRequest(int PageNumber = 1, int PageSize = 10);