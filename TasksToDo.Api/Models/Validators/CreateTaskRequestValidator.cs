using FluentValidation;
using TasksToDo.Models.DTOs.Requests;

namespace TasksToDo.Models.Validators;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
    }
}