using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Interfaces.Services;

public interface ITaskService
{
    public Task<PagedResponse<TaskResponse>> GetAllAsync(PagedRequest request);
    public Task<TaskResponse?> GetByIdAsync(Guid id);
}