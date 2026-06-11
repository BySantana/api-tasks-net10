using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Interfaces.Services;

public interface ITagService
{
    public Task<bool> DeleteAsync(Guid id);
}