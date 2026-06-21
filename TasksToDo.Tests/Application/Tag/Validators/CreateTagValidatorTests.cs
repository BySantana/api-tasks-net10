using FluentValidation.TestHelper;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;

namespace TasksToDo.Tests.Application.Tag.Validators;

public class CreateTagValidatorTests
{
    private readonly CreateTagValidator _validator = new();

    [Fact]
    public void TituloVazio_DeveTerErro()
    {
        var command = new CreateTagCommand("");
        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void TituloGrande_DeveTerErro()
    {
        var command = new CreateTagCommand(
            @"Texto grande com mais de 200 caracteres. Lorem Ipson
            Lorem Ipson Lorem Ipson Lorem Ipson Lorem Ipson Lorem Ipson"
        );

        var result = _validator.TestValidate(command);
        result.ShouldHaveValidationErrorFor(x => x.Title);
    }

    [Fact]
    public void TituloValido_NaoDeveTerErro()
    {
        var command = new CreateTagCommand("Trabalho");
        var result = _validator.TestValidate(command);
        result.ShouldNotHaveValidationErrorFor(x => x.Title);
    }
}