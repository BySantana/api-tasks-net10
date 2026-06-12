using MediatR;

namespace TasksToDo.Application.Services.Tag.Commands.DeleteTag;

public record DeleteTagCommand(Guid Id) : IRequest<bool>;