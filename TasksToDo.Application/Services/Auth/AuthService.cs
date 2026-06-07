using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Auth;

public class AuthService(IApplicationDbContext context, IConfiguration configuration) : IAuthService
{
    private readonly IApplicationDbContext _context = context;
    private readonly IConfiguration _configuration = configuration;

    public async Task<TokenResponse> LoginAsync(UserModel request)
    {
        var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email)
                   ?? throw new UnauthorizedAccessException("Invalid credentials");

        var validPassword = BCrypt.Net.BCrypt.Verify(request.Password, user.Password);

        return !validPassword
            ? throw new UnauthorizedAccessException("Invalid credentials")
            : GenerateToken(user);
    }

    public async Task<TokenResponse> RegisterAsync(UserModel request)
    {
        var existingUser = await _context.Users.FindAsync(request.Id);

        if (existingUser is not null)
            throw new InvalidOperationException("Email already in use");

        var hash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var user = new UserModel(request.Email, hash);

        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();

        return GenerateToken(user);
    }

    private TokenResponse GenerateToken(UserModel user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var expiresAt = DateTime.UtcNow.AddMinutes(
            int.Parse(_configuration["Jwt:ExpiresInMinutes"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new TokenResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            expiresAt);
    }
}