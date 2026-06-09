using MediatR;

namespace TasksToDo.Application.Services.Tag.Commands.CreateTag;

public sealed record CreateTagCommand(string Title) : IRequest<Guid>;