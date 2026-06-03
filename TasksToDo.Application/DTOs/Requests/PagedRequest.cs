namespace TasksToDo.Application.DTOs.Requests;

public record PagedRequest(int PageNumber = 1, int PageSize = 10);