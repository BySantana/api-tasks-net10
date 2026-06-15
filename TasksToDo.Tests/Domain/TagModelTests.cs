using TasksToDo.Domain.Models;

namespace TasksToDo.Tests.Domain;

public class TagModelTests
{
    #region Create
    
    [Fact]
    public void Create_ComNomeValido_CriaTag()
    {
        var tag = new TagModel("Estudo .Net");

        Assert.Equal("Estudo .Net", tag.Title);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void Create_ComNomeVazio_LancaErro(string? title)
    {
        Assert.Throws<ArgumentNullException>(() => new TagModel(title));
    }
    
    #endregion
    
    #region ChangeTag

    [Fact]
    public void ChangeTag_ComTituloValido_AlteraTitulo()
    {
        // Arrange
        var tag = new TagModel("Estudo .Net");

        // Act
        tag.ChangeTag("Novo");

        // Assert
        Assert.Equal("Novo", tag.Title);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void ChangeTag_ComTituloVazio_LancaErro(string? title)
    {
        // Arrange
        var tag = new TagModel("Estudo .Net");

        // Act + Assert
        Assert.Throws<ArgumentNullException>(() => tag.ChangeTag(title));
    }

    #endregion
}