using TasksToDo.Domain.Models;

namespace TasksToDo.Tests.Domain;

public class TaskModelTests
{
    #region Create
    
    [Fact]
    public void Create_ComNomeValido_CriaTask()
    {
        var tag = new TaskModel("Criar API", "Description Teste");

        Assert.Equal("Criar API", tag.Title);
        Assert.Equal("Description Teste", tag.Description);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_ComNomeVazio_LancaErro(string? title)
    {
        Assert.Throws<ArgumentNullException>(() => new TaskModel(title, null));
    }
    
    #endregion
}