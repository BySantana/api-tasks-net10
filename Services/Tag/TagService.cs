using Microsoft.EntityFrameworkCore;
using TasksToDo.Data;
using TasksToDo.Models;
using TasksToDo.Models.DTOs.Requests;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Tag;

public class TagService(AppDbContext context) : ITagService
{
    private readonly AppDbContext _context = context;
    private static TagResponse Project(TagModel t) => new(
        t.Id,
        t.Title,
        t.CreatedAt);

    public async Task<PagedResponse<TagResponse>> GetAllAsync(PagedRequest request)
    {
        var query = _context.Tags.AsQueryable();

        var totalCount = await query.CountAsync();

        var data = await _context.Tags
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => Project(c))
            .OrderBy(o => o.CreatedAt)
            .ToListAsync();

        return new PagedResponse<TagResponse>(
            data,
            request.PageNumber,
            request.PageSize, 
            totalCount,
            (int)Math.Ceiling(totalCount / (double)request.PageSize));
    }

    public async Task<TagResponse?> GetByIdAsync(Guid id)
    {
        return await _context.Tags
            .Where(x => x.Id == id)
            .Select(x => Project(x))
            .FirstOrDefaultAsync();
    }

    public async Task<TagResponse> CreateAsync(TagModel tagModel)
    {
        await _context.Tags.AddAsync(tagModel);
        await _context.SaveChangesAsync();

        return Project(tagModel);
    }

    public async Task<TagResponse?> UpdateAsync(Guid id, TagModel tagModel)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
            return null;

        tag.ChangeTag(tagModel.Title);
        await _context.SaveChangesAsync();

        return Project(tag);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var tag = await _context.Tags.FindAsync(id);

        if (tag == null)
            return false;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync();

        return true;
    }
}