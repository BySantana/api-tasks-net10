using MediatR;
using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Task.Utils;

namespace TasksToDo.Application.Services.Task.Queries.GetAllTasks;

public sealed class GetAllTasksHandler(IApplicationDbContext context) : IRequestHandler<GetAllTasksQuery, PagedResponse<TaskResponse>>
{
    private readonly IApplicationDbContext _context = context;
    
    public async Task<PagedResponse<TaskResponse>> Handle(GetAllTasksQuery cmd, CancellationToken ct)
    {
        var query = _context.Tasks.AsQueryable();

        var totalCount = await query.CountAsync(ct);

        var data = await query
            .OrderBy(o => o.CreatedAt)
            .Skip((cmd.PagedRequest.PageNumber - 1) * cmd.PagedRequest.PageSize)
            .Take(cmd.PagedRequest.PageSize)
            .Include(t => t.Tags)
            .Select(x => MapTaskResponse.Project(x))
            .ToListAsync(ct);

        return new PagedResponse<TaskResponse>(
            data,
            cmd.PagedRequest.PageNumber,
            cmd.PagedRequest.PageSize,
            totalCount,
            (int)Math.Ceiling(totalCount / (double)cmd.PagedRequest.PageSize));
    }
}