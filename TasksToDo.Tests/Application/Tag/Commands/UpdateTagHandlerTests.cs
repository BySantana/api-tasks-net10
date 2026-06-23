using Microsoft.EntityFrameworkCore;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;
using TasksToDo.Application.Services.Tag.Commands.UpdateTag;
using TasksToDo.Infrastructure.Data;

namespace TasksToDo.Tests.Application.Tag.Commands;

public class UpdateTagHandlerTests
{
    private static AppDbContext CriarContexto()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        return new AppDbContext(options);
    }
    
    [Fact]
    public async Task Handle_CommandValido_AtualizaTag()
    {
        // Arrange
        await using var context = CriarContexto();
        var handler = new UpdateTagHandler(context);
        var handlerCreate = new CreateTagHandler(context);
        
        var idTag = await handlerCreate.Handle(new CreateTagCommand("Trabalho"), CancellationToken.None);

        // Act
        await handler.Handle(new UpdateTagCommand(idTag,"Trabalho Atualizado"), CancellationToken.None);

        // Assert
        var tag = await context.Tags.FirstOrDefaultAsync();
        Assert.NotNull(tag);
        Assert.Equal("Trabalho Atualizado", tag.Title);
    }
    
    [Fact]
    public async Task Handle_TagInexistente_LancaExcecao()
    {
        // Arrange
        await using var context = CriarContexto();
        var handler = new UpdateTagHandler(context);
        var command = new UpdateTagCommand(Guid.NewGuid(), "Novo Título");

        // Act + Assert
        Assert.Null(await handler.Handle(command, CancellationToken.None));
    }
}