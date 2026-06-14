using MediatR;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Task.Queries.GetAllTasks;

public sealed record GetAllTasksQuery(PagedRequest PagedRequest) : IRequest<PagedResponse<TaskResponse>>;