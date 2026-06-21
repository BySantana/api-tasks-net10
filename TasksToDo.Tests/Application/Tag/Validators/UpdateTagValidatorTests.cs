using FluentValidation.TestHelper;
using TasksToDo.Application.Services.Tag.Commands.UpdateTag;

namespace TasksToDo.Tests.Application.Tag.Validators;

public class UpdateTagValidatorTests
{
    private readonly UpdateTagValidator _validator = new(); 
    
    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData(null)]
    public void TituloVazio_DeveTerErro(string? title)
    {
        var command = new UpdateTagCommand(Guid.Empty, title);
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void TituloGrande_DeveTerErro()
    {
        var command = new UpdateTagCommand(Guid.Empty,
            @"Texto grande com mais de 200 caracteres. Lorem Ipson
            Lorem Ipson Lorem Ipson Lorem Ipson Lorem Ipson Lorem Ipson"
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void TituloValido_NaoDeveTerErro()
    {
        var command = new UpdateTagCommand(Guid.Empty,"Trabalho");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}