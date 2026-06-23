using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Abstractions;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;
using TasksToDo.Infrastructure.Data;

namespace TasksToDo.Tests.Application.Tag.Commands;

public class CreateTagHandlerTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
    
    [Fact]
    public async Task Handle_CommandValido_PersisteTag()
    {
        // Arrange
        await using var context = CriarContexto();
        var handler = new CreateTagHandler(context);

        // Act
        await handler.Handle(new CreateTagCommand("Trabalho"), CancellationToken.None);

        // Assert
        var tag = await context.Tags.FirstOrDefaultAsync();
        Assert.NotNull(tag);
        Assert.Equal("Trabalho", tag.Title);
    }
}