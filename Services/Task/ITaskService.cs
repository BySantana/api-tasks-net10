using TasksToDo.Models;
using TasksToDo.Models.DTOs.Requests;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Task;

public interface ITaskService
{
    public Task<PagedResponse<TaskResponse>> GetAllAsync(PagedRequest request);
    public Task<TaskResponse?> GetByIdAsync(Guid id);
    public Task<TaskResponse> CreateAsync(TaskModel model);
    public Task<TaskResponse?> UpdateAsync(Guid id, TaskModel model);
    public Task<bool> DeleteAsync(Guid id);
}