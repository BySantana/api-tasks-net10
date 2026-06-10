using MediatR;
using TasksToDo.Application.DTOs.Responses;

namespace TasksToDo.Application.Services.Tag.Queries.GetTagById;

public record GetTagByIdQuery(Guid Id) : IRequest<TagResponse?>;