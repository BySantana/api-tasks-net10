using TasksToDo.Models;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Tag;

public interface ITagService
{
    public Task<IList<TagResponse>> GetAllAsync();
    public Task<TagResponse> CreateAsync(TagModel tag);
    // public Task<TagModel> UpdateAsync(Guid id, TagModel tag);
    // public Task<TagModel> DeleteAsync(Guid id);
}