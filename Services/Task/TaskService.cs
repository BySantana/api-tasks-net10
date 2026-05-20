using Microsoft.EntityFrameworkCore;
using TasksToDo.Data;
using TasksToDo.Models;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Task;

public class TaskService(AppDbContext context) : ITaskService
{
    private readonly AppDbContext _context = context;

    public async Task<IList<TaskResponse>> GetAllAsync()
    {
        return await _context.Tasks
            .Select(x =>
                new TaskResponse(x.Title, x.Description,
                    new TagResponse(x.Tag.Id, x.Tag.Title)))
            .ToListAsync();
    }

    public async Task<TaskResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Where(x => x.Id == id)
            .Select(x => new TaskResponse(
                x.Title,
                x.Description,
                new TagResponse(x.Tag.Id, x.Tag.Title)))
            .FirstOrDefaultAsync();
    }

    public async Task<TaskResponse> CreateAsync(TaskModel taskModel)
    {
        await _context.Tasks.AddAsync(taskModel);
        await _context.SaveChangesAsync();

        return new TaskResponse(
            taskModel.Title,
            taskModel.Description,
            null!);
    }

    public async Task<TaskResponse?> UpdateAsync(Guid id, TaskModel model)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task is null)
            return null;

        task.ChangeTask(model.Title, model.Description, model.TagId);
        await _context.SaveChangesAsync();

        return new TaskResponse(task.Title, task.Description, null!);
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
}