using MediatR;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Task.Commands.UpdateTask;

public sealed record UpdateTaskCommand(Guid Id, string Title, string? Description) : IRequest<TaskResponse?>;