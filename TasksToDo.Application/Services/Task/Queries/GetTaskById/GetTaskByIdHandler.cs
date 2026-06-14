using MediatR;
using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Task.Utils;

namespace TasksToDo.Application.Services.Task.Queries.GetTaskById;

public sealed class GetTaskByIdHandler(IApplicationDbContext context) : IRequestHandler<GetTaskByIdQuery, TaskResponse?>
{
    private readonly IApplicationDbContext _context = context;
    
    public async Task<TaskResponse?> Handle(GetTaskByIdQuery cmd, CancellationToken ct)
    {
        return await _context.Tasks
            .Where(x => x.Id == cmd.TaskId)
            .Include(x => x.Tags)
            .Select(x => MapTaskResponse.Project(x))
            .FirstOrDefaultAsync(ct);
    }
}