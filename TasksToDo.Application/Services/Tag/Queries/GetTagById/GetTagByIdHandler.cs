using MediatR;
using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Tag.Utils;

namespace TasksToDo.Application.Services.Tag.Queries.GetTagById;

public sealed class GetTagByIdHandler : IRequestHandler<GetTagByIdQuery, TagResponse?>
{
    private readonly IApplicationDbContext _context;

    public GetTagByIdHandler(IApplicationDbContext context)
        => _context = context;
    
    public async Task<TagResponse?> Handle(GetTagByIdQuery request, CancellationToken ct)
    {
        return await _context.Tags
            .Where(x => x.Id == request.Id)
            .Select(x => MapResponse.Project(x))
            .FirstOrDefaultAsync(ct);
    }
}