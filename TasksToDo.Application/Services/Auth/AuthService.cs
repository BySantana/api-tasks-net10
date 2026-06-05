using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Domain.Models;
using TasksToDo.Infrastructure.Data;

namespace TasksToDo.Application.Services.Auth;

public class AuthService(AppDbContext context) : IAuthService
{
    private readonly AppDbContext _context = context;
    
    public Task<TokenResponse> LoginAsync(UserModel request)
    {
        throw new NotImplementedException();
    }

    public Task<TokenResponse> RegisterAsync(UserModel request)
    {
        throw new NotImplementedException();
        // var existingUser = await _repository.GetByEmailAsync(request.Email);
        // if (existingUser is not null)
        //     throw new InvalidOperationException("Email already in use");
        //
        // var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        // var user = new User(request.Email, hash);
        //
        // await _repository.AddAsync(user);
        // await _context.SaveChangesAsync(); // ou UnitOfWork
        //
        // return GenerateToken(user);
    }
}