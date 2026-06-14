using MediatR;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Task.Commands.AddTagToTask;

public sealed record AddTagToTaskCommand(Guid TaskId, Guid TagId) : IRequest<TaskResponse>;