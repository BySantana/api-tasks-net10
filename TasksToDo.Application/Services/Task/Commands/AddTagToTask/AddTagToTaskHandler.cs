using MediatR;
using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Task.Utils;

namespace TasksToDo.Application.Services.Task.Commands.AddTagToTask;

public sealed class AddTagToTaskHandler(IApplicationDbContext context) : IRequestHandler<AddTagToTaskCommand, TaskResponse?>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<TaskResponse?> Handle(AddTagToTaskCommand cmd, CancellationToken ct)
    {
        var task = await _context.Tasks
                       .Include(t => t.Tags)
                       .FirstOrDefaultAsync(t => t.Id == cmd.TaskId, ct)
                   ?? throw new KeyNotFoundException("Task not found");

        var tag = await _context.Tags.FindAsync(cmd.TagId, ct)
                  ?? throw new KeyNotFoundException("Tag not found");

        task.AddTag(tag);
        await _context.SaveChangesAsync(ct);
        
        return MapTaskResponse.Project(task);
    }
}