using MediatR;
using TasksToDo.Application.Abstractions;

namespace TasksToDo.Application.Services.Task.Commands.DeleteTask;

public class DeleteTaskHandler(IApplicationDbContext context) : IRequestHandler<DeleteTaskCommand, bool>
{
    private readonly IApplicationDbContext _context = context;
    public async Task<bool> Handle(DeleteTaskCommand cmd, CancellationToken ct)
    {
        var task = await _context.Tasks.FindAsync(cmd.Id);

        if (task is null)
            return false;

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync(ct);

        return true;
    }
}