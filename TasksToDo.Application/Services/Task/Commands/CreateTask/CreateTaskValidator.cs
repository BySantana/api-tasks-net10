using FluentValidation;

namespace TasksToDo.Application.Services.Task.Commands.CreateTask;

public class CreateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public CreateTaskValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
    }
}