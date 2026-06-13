using MediatR;

namespace TasksToDo.Application.Services.Task.Commands.CreateTask;

public sealed record CreateTaskCommand(string Title, string? Description = null) : IRequest<Guid>;