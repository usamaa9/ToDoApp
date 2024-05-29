using FluentValidation;
using ToDoApp.Application.Commands;

namespace ToDoApp.Application.Validators;

/// <summary>
/// Represents a validator for the <see cref="CreateToDoCommand"/> command.
/// </summary>
public class CreateTodoCommandValidator : AbstractValidator<CreateToDoCommand>
{
    /// <summary>
    /// /// Initializes a new instance of the <see cref="CreateTodoCommandValidator"/> class. ///
    /// </summary>
    public CreateTodoCommandValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.DueDate).NotEmpty().Must(BeValidDate);
    }

    private bool BeValidDate(DateTime dueDate)
    {
        return dueDate != default;
    }
}
