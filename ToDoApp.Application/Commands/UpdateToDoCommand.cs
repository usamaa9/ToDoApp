using ToDoApp.Application.Exceptions;

namespace ToDoApp.Application.Commands;

/// <summary>
/// Represents a command to update a ToDo item.
/// </summary>
/// <param name="Title">The new title of the ToDo item.</param>
/// <param name="Description">The new description of the ToDo item.</param>
/// <param name="DueDate">The new due date of the ToDo item.</param>
/// <param name="IsCompleted">The new completion status of the ToDo item.</param>
public record UpdateToDoCommand(string Title, string Description, DateTime DueDate, bool IsCompleted) : IRequest<ToDoResponse>
{
    /// <summary>
    /// Gets or sets the ID of the ToDo item.
    /// </summary>
    public Guid Id { get; set; }
}

internal sealed class UpdateToDoCommandHandler(IToDoRepository repository) : IRequestHandler<UpdateToDoCommand, ToDoResponse>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<ToDoResponse> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id) ?? throw new ToDoNotFoundException(request.Id);

        if (entity.IsCompleted && !request.IsCompleted)
        {
            entity.MarkIncomplete();
        }
        else if (!entity.IsCompleted && request.IsCompleted)
        {
            entity.MarkComplete();
        }

        entity.UpdateDetails(request.Title, request.Description, request.DueDate);

        await _repository.UpdateAsync(entity);
        return new ToDoResponse(entity);
    }
}
