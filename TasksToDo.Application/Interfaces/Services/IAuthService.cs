using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Interfaces.Services;

public interface IAuthService
{
    public Task<TokenResponse> LoginAsync(UserModel request);
    public Task<TokenResponse> RegisterAsync(UserModel request);
}