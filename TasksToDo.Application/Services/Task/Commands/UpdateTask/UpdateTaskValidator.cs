using FluentValidation;
using TasksToDo.Application.Services.Task.Commands.CreateTask;

namespace TasksToDo.Application.Services.Task.Commands.UpdateTask;

public class UpdateTaskValidator : AbstractValidator<CreateTaskCommand>
{
    public UpdateTaskValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
    }
}