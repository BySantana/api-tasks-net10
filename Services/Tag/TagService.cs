using Microsoft.EntityFrameworkCore;
using TasksToDo.Data;
using TasksToDo.Models;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Tag;

public class TagService(AppDbContext context) : ITagService
{
    private readonly AppDbContext _context = context;

    public async Task<IList<TagResponse>> GetAllAsync()
    {
        return await _context.Tags
            .Select(c => new TagResponse(c.Id, c.Title))
            .ToListAsync();
    }

    public async Task<TagResponse> CreateAsync(TagModel tagModel)
    {
        await _context.Tags.AddAsync(tagModel);
        await _context.SaveChangesAsync();

        return new TagResponse(tagModel.Id, tagModel.Title);
    }
}