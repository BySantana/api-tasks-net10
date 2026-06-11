using MediatR;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Tag.Commands.UpdateTag;

public sealed record UpdateTagCommand(Guid Id, string Title) : IRequest<TagResponse?>;