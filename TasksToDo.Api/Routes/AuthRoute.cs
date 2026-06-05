using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Interfaces.Services;
using TasksToDo.Domain.Models;

namespace TasksToDo.Routes;

public static class AuthRoute
{
    public static void MapAuthRoute(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/auth").WithTags("Auth");

        group.MapPost("/register", Register).AllowAnonymous();
        group.MapPost("/login", Login).AllowAnonymous();
    }

    private static async Task<IResult> Register([FromBody] RegisterRequest request,
        [FromServices] IValidator<RegisterRequest> validator, [FromServices] IAuthService service)
    {
        var validation = await validator.ValidateAsync(request);
        if (!validation.IsValid)
            return Results.ValidationProblem(validation.ToDictionary());

        var userModel = new UserModel(request.Email, request.Password);

        var token = await service.RegisterAsync(userModel);
        return Results.Ok(token);
    }

    private static async Task<IResult> Login([FromBody] LoginRequest request,
        [FromServices] IAuthService service)
    {
        var userModel = new UserModel(request.Email, request.Password);

        var token = await service.LoginAsync(userModel);
        return Results.Ok(token);
    }
}