using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Interfaces.Services;

public interface ITaskService
{
    public Task<PagedResponse<TaskResponse>> GetAllAsync(PagedRequest request);
    public Task<TaskResponse?> GetByIdAsync(Guid id);
    public Task<TaskResponse> CreateAsync(TaskModel model);
    public Task<TaskResponse?> UpdateAsync(Guid id, TaskModel model);
    public Task<bool> DeleteAsync(Guid id);
    public Task<TaskResponse> AddTagAsync(Guid taskId, Guid tagId);
    public Task<TaskResponse> RemoveTagAsync(Guid taskId, Guid tagId);
}