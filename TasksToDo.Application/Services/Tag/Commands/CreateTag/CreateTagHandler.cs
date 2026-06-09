using MediatR;
using TasksToDo.Application.Abstractions;
using TasksToDo.Domain.Models;

namespace TasksToDo.Application.Services.Tag.Commands.CreateTag;

public sealed class CreateTagHandler(IApplicationDbContext context) : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<Guid> Handle(CreateTagCommand cmd, CancellationToken ct)
    {
        var tag = new TagModel(cmd.Title);

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(ct);

        return tag.Id;
    }
}