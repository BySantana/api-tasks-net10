using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Task;

public class TaskService(IApplicationDbContext context) : ITaskService
{
    private readonly IApplicationDbContext _context = context;

    private static TaskResponse Project(TaskModel t) => new(
        t.Title,
        t.Description,
        t.CreatedAt,
        t.Tags.Select(tag => new TagResponse(tag.Id, tag.Title, tag.CreatedAt)));

    public async Task<PagedResponse<TaskResponse>> GetAllAsync(PagedRequest request)
    {
        var query = _context.Tasks.AsQueryable();

        var totalCount = await query.CountAsync();

        var data = await query
            .OrderBy(o => o.CreatedAt)
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Include(t => t.Tags)
            .Select(x => Project(x))
            .ToListAsync();

        return new PagedResponse<TaskResponse>(
            data,
            request.PageNumber,
            request.PageSize,
            totalCount,
            (int)Math.Ceiling(totalCount / (double)request.PageSize));
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Where(x => x.Id == id)
            .Select(x => Project(x))
            .FirstOrDefaultAsync();
    }

    public async Task<TaskResponse> CreateAsync(TaskModel taskModel)
    {
        await _context.Tasks.AddAsync(taskModel);
        await _context.SaveChangesAsync();

        return Project(taskModel);
    }

    public async Task<TaskResponse?> UpdateAsync(Guid id, TaskModel model)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
            return null;

        task.ChangeTask(model.Title, model.Description);
        await _context.SaveChangesAsync();

        return Project(task);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<TaskResponse> AddTagAsync(Guid taskId, Guid tagId)
    {
        var task = await _context.Tasks
                       .Include(t => t.Tags)
                       .FirstOrDefaultAsync(t => t.Id == taskId)
                   ?? throw new KeyNotFoundException("Task not found");

        var tag = await _context.Tags.FindAsync(tagId)
                  ?? throw new KeyNotFoundException("Tag not found");

        task.AddTag(tag);
        await _context.SaveChangesAsync();
        
        return Project(task);
    }

    public async Task<TaskResponse> RemoveTagAsync(Guid taskId, Guid tagId)
    {
        var task = await _context.Tasks
                       .Include(t => t.Tags)
                       .FirstOrDefaultAsync(t => t.Id == taskId)
                   ?? throw new KeyNotFoundException("Task not found");

        var tag = await _context.Tags.FindAsync(tagId)
                  ?? throw new KeyNotFoundException("Tag not found");

        task.RemoveTag(tag);
        await _context.SaveChangesAsync();
        
        return Project(task);
    }
}