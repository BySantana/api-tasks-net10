using MediatR;
using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Tag.Queries.GetAllTags;

public class GetAllTagsHandler : IRequestHandler<GetAllTagsQuery, PagedResponse<TagResponse>>
{
    private readonly IApplicationDbContext _context;

    public GetAllTagsHandler(IApplicationDbContext context)
        => _context = context;
    
    private static TagResponse Project(TagModel t) => new(
        t.Id,
        t.Title,
        t.CreatedAt);

    public async Task<PagedResponse<TagResponse>> Handle(
        GetAllTagsQuery query, CancellationToken ct)
    {
        var tags = _context.Tags.AsQueryable();

        var totalCount = await tags.CountAsync(ct);

        var data = await _context.Tags
            .OrderBy(o => o.CreatedAt)
            .Skip((query.PagedRequest.PageNumber - 1) * query.PagedRequest.PageSize)
            .Take(query.PagedRequest.PageSize)
            .Select(c => Project(c))
            .ToListAsync(ct);

        return new PagedResponse<TagResponse>(
            data,
            query.PagedRequest.PageNumber,
            query.PagedRequest.PageSize,
            totalCount,
            (int)Math.Ceiling(totalCount / (double)query.PagedRequest.PageSize));
    }
}