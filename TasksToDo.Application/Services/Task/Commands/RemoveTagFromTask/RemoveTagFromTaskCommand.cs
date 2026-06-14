using MediatR;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Task.Commands.RemoveTagFromTask;

public sealed record RemoveTagFromTaskCommand(Guid TaskId, Guid TagId) :  IRequest<TaskResponse?>;