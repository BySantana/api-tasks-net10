using FluentValidation;

namespace TasksToDo.Application.Services.Task.Commands.AddTagToTask;

public class AddTagToTaskValidator : AbstractValidator<AddTagToTaskCommand>
{
    public AddTagToTaskValidator()
    {
        RuleFor(request => request.TaskId)
            .NotEmpty().WithMessage("Task ID is required");

        RuleFor(request => request.TaskId)
            .NotEmpty().WithMessage("Tag ID is required");
    }
}