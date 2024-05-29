using FluentValidation.Results;
using SharpGrip.FluentValidation.AutoValidation.Endpoints.Results;
using SharpGrip.FluentValidation.AutoValidation.Shared.Extensions;
using System.Net;

namespace ToDoApp.Api;

/// <summary>
/// Represents a custom result factory.
/// </summary>
public class ValidationResultFactory : IFluentValidationAutoValidationResultFactory
{
    /// <inheritdoc/>
    public IResult CreateResult(EndpointFilterInvocationContext context, ValidationResult validationResult)
    {
        var validationProblemErrors = validationResult.ToValidationProblemErrors();

        return Results.ValidationProblem(validationProblemErrors, statusCode: (int)HttpStatusCode.BadRequest, title: "Validation errors. See errors object.");
    }
}
