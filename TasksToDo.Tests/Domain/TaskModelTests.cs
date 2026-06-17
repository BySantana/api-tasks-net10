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
    
    #region ChangeTask
    
    [Fact]
    public void ChangeTask_ComTituloValido_AlteraTitulo()
    {
        // Arrange
        var task = new TaskModel("Teste", "Teste Description");

        // Act
        task.ChangeTask("Novo", "Novo Description");

        // Assert
        Assert.Equal("Novo", task.Title);
        Assert.Equal("Novo Description", task.Description);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ChangeTask_ComTituloVazio_LancaErro(string? title)
    {
        // Arrange
        var tag = new TagModel("Teste");

        // Act + Assert
        Assert.Throws<ArgumentNullException>(() => tag.ChangeTag(title));
    }
    
    #endregion

    #region ChangeTagsToTask

    [Fact]
    public void AddTagToTask_ComTagValida_AdicionaTag()
    {
        var task = new TaskModel("Task Teste", null);
        var tag = new TagModel("Tag Teste");
        
        task.AddTag(tag);
        
        Assert.Contains(tag, task.Tags);
    }
    
    [Fact]
    public void AddTagToTask_ComTagInvalida_LancaErro()
    {
        var task = new TaskModel("Task Teste", null);
        
        Assert.Throws<ArgumentNullException>(() => task.AddTag(null));
    }

    [Fact]
    public void RemoveTagFromTask_ComTagValida_RemoveTag()
    {
        var task = new TaskModel("Task Teste", null);
        var tag = new TagModel("Tag Teste");
        task.AddTag(tag);
        
        task.RemoveTag(tag);
        
        Assert.DoesNotContain(tag, task.Tags);
    }
    
    [Fact]
    public void RemoveTagFromTask_ComTagInvalida_LancaErro()
    {
        var task = new TaskModel("Task Teste", null);
        var tag = new TagModel("Tag Teste");
        task.AddTag(tag);
        
        var newTag = new TagModel("Tag nova teste");
        
        task.RemoveTag(newTag);
        
        Assert.Contains(tag, task.Tags);
    }

    #endregion
}