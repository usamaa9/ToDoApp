using FluentValidation;
using ToDoApp.Application.Commands;

namespace ToDoApp.Application.Validators;

/// <summary>
/// Represents a validator for the <see cref="CreateToDoCommand"/> command.
/// </summary>
public class CreateToDoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    /// <summary>
    /// /// Initializes a new instance of the <see cref="CreateToDoCommandValidator"/> class. ///
    /// </summary>
    public CreateToDoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.DueDate).NotEmpty().Must(x => x != default);
    }
}
