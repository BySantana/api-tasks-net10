using MediatR;
using TasksToDo.Application.Abstractions;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Task.Commands.CreateTask;

public class CreateTaskHandler(IApplicationDbContext context) : IRequestHandler<CreateTaskCommand, Guid>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Guid> Handle(CreateTaskCommand cmd, CancellationToken ct)
    {
        var task = new TaskModel(cmd.Title, cmd.Description);

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(ct);

        return task.Id;
    }
}