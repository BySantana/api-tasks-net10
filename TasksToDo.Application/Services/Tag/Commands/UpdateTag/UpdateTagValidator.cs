using FluentValidation;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;

namespace TasksToDo.Application.Services.Tag.Commands.UpdateTag;

public class UpdateTagValidator : AbstractValidator<CreateTagCommand>
{
    public UpdateTagValidator()
    {
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(200).WithMessage("Title must not exceed 200 characters");
    }
}