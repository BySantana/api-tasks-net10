using MediatR;
using TasksToDo.Application.Abstractions;

namespace TasksToDo.Application.Services.Tag.Commands.DeleteTag;

public class CreateTagHandler(IApplicationDbContext context) : IRequestHandler<DeleteTagCommand, bool>
{
    private readonly IApplicationDbContext _context = context;
    
    public async Task<bool> Handle(DeleteTagCommand request, CancellationToken ct)
    {
        var tag = await _context.Tags.FindAsync(request.Id, ct);

        if (tag == null)
            return false;

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(ct);

        return true;
    }
}