using MediatR;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Task.Commands.DeleteTask;

public sealed record DeleteTaskCommand(Guid Id) : IRequest<bool>;