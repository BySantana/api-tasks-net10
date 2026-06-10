using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Tag;

public class TagService(IApplicationDbContext context) : ITagService
{
    private readonly IApplicationDbContext _context = context;
    private static TagResponse Project(TagModel t) => new(
        t.Id,
        t.Title,
        t.CreatedAt);

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