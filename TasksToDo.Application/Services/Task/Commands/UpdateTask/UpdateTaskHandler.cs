using MediatR;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Task.Utils;

namespace TasksToDo.Application.Services.Task.Commands.UpdateTask;

public class UpdateTaskHandler(IApplicationDbContext context) : IRequestHandler<UpdateTaskCommand, TaskResponse?>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<TaskResponse?> Handle(UpdateTaskCommand cmd, CancellationToken ct)
    {
        var task = await _context.Tasks.FindAsync(cmd.Id, ct);

        if (task is null)
            return null;

        task.ChangeTask(cmd.Title, cmd.Description);
        await _context.SaveChangesAsync(ct);

        return MapTaskResponse.Project(task);
    }
}