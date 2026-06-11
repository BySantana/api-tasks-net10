using MediatR;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.DTOs.Responses;
using TasksToDo.Application.Services.Tag.Utils;

namespace TasksToDo.Application.Services.Tag.Commands.UpdateTag;

public sealed class UpdateTagHandler(IApplicationDbContext context) : IRequestHandler<UpdateTagCommand, TagResponse?>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<TagResponse?> Handle(UpdateTagCommand cmd, CancellationToken ct)
    {
        var tag = await _context.Tags.FindAsync(cmd.Id, ct);

        if (tag == null)
            return null;

        tag.ChangeTag(cmd.Title);
        await _context.SaveChangesAsync(ct);

        return MapResponse.Project(tag);
    }
}