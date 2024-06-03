using SharpGrip.FluentValidation.AutoValidation.Endpoints.Extensions;
using ToDoApp.Application.Commands;
using ToDoApp.Application.Requests;
using ToDoApp.Application.Responses;

namespace ToDoApp.Api;

/// <summary>
/// Defines the endpoints for the ToDo application.
/// </summary>
public static class Endpoints
{
    /// <summary>
    /// Maps all the endpoints for the ToDo application.
    /// </summary>
    /// <param name="app">The WebApplication instance.</param>
    /// <returns>The WebApplication instance with all the endpoints mapped.</returns>
    public static WebApplication MapAllEndpoints(this WebApplication app)
    {
        // Create ToDo item
        app.MapPost("/api/todo", async (IMediator mediator, CreateToDoCommand command) =>
        {
            var result = await mediator.Send(command);
            return Results.Created($"/api/todo/{result.Id}", result);
        })
        .WithOpenApi(configureOperation: operation =>
        {
            operation.Summary = "Creates a new ToDo item.";
            operation.Description = "Creates a new ToDo item in the system.";
            return operation;
        })
        .Produces<ToDoResponse>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .AddFluentValidationAutoValidation();

        // Get ToDo item
        app.MapGet("/api/todo/{id:guid}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new GetToDoQuery(id));
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithOpenApi(configureOperation: operation =>
        {
            operation.Summary = "Retrieves a ToDo item by its unique identifier.";
            operation.Description = "Retrieves a ToDo item by its unique identifier.";
            return operation;
        })
        .Produces<ToDoResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Update ToDo item
        app.MapPut("/api/todo/{id:guid}", async (IMediator mediator, Guid id, UpdateToDoRequest request) =>
        {
            var command = new UpdateToDoCommand(id, request.Title, request.Description, request.DueDate, request.IsComplete);
            var result = await mediator.Send(command);
            return result != null ? Results.Ok(result) : Results.NotFound();
        })
        .WithOpenApi(configureOperation: operation =>
        {
            operation.Summary = "Updates a ToDo item by its unique identifier.";
            operation.Description = "Updates a ToDo item by its unique identifier.";
            return operation;
        })
        .Produces<ToDoResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        // Delete ToDo item
        app.MapDelete("/api/todo/{id:guid}", async (IMediator mediator, Guid id) =>
        {
            var result = await mediator.Send(new DeleteToDoCommand(id));
            return result ? Results.NoContent() : Results.NotFound();
        })
        .WithOpenApi(configureOperation: operation =>
        {
            operation.Summary = "Deletes a ToDo item by its unique identifier.";
            operation.Description = "Deletes a ToDo item by its unique identifier.";
            return operation;
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound);

        // List all ToDo items
        app.MapGet("/api/todo", async (IMediator mediator) =>
        {
            var result = await mediator.Send(new ListToDosQuery());
            return Results.Ok(result);
        })
        .WithOpenApi(configureOperation: operation =>
        {
            operation.Summary = "Retrieves a list of all ToDo items.";
            operation.Description = "Retrieves a list of all ToDo items.";
            return operation;
        })
        .Produces<List<ToDoResponse>>(StatusCodes.Status200OK);

        return app;
    }
}
