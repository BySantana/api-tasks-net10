using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Interfaces.Services;

public interface ITagService
{
    public Task<TagResponse?> GetByIdAsync(Guid id);
    public Task<TagResponse> CreateAsync(TagModel tag);
    public Task<TagResponse?> UpdateAsync(Guid id, TagModel tagModel);
    public Task<bool> DeleteAsync(Guid id);
}