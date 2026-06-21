using FluentValidation;
using TasksToDo.Application.DTOs.Requests;
using TasksToDo.Application.Services.Tag.Commands.CreateTag;

namespace TasksToDo.Application.Services.Tag.Commands.UpdateTag;

public class UpdateTagValidator : AbstractValidator<UpdateTagCommand>
{
    public UpdateTagValidator()
    {
        RuleFor(request => request.Id)
            .NotEmpty().WithMessage("Title is required");
        
        RuleFor(request => request.Title)
            .NotEmpty().WithMessage("Title is required")
            .MaximumLength(100).WithMessage("Title must not exceed 100 characters");
    }
}