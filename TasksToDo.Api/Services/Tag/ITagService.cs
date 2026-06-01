using TasksToDo.Models;
using TasksToDo.Models.DTOs.Requests;
using TasksToDo.Models.DTOs.Responses;

namespace TasksToDo.Services.Tag;

public interface ITagService
{
    public Task<PagedResponse<TagResponse>> GetAllAsync(PagedRequest request);
    public Task<TagResponse?> GetByIdAsync(Guid id);
    public Task<TagResponse> CreateAsync(TagModel tag);
    public Task<TagResponse?> UpdateAsync(Guid id, TagModel tagModel);
    public Task<bool> DeleteAsync(Guid id);
}