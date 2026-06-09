using MediatR;
using TasksToDo.Application.Abstractions;

namespace TasksToDo.Application.Services.Tag.Commands.CreateTag;

public sealed class CreateTagHandler : IRequestHandler<CreateTagCommand, Guid>
{
    private readonly IApplicationDbContext _context;

    public CreateTagHandler(IApplicationDbContext context)
        => _context = context;

    public async Task<Guid> Handle(CreateTagCommand cmd, CancellationToken ct)
    {
        var tag = Tag.Create(cmd.title);

        _context.Tags.Add(tag);
        await _context.SaveChangesAsync(ct);

        return tag.Id;
    }
}