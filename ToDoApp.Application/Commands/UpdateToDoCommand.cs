using ToDoApp.Application.Exceptions;

namespace ToDoApp.Application.Commands;

/// <summary>
/// Represents a command to update a ToDo item.
/// </summary>
/// <param name="Id">The unique identifier of the ToDo item to update.</param>
/// <param name="Title">The new title of the ToDo item.</param>
/// <param name="Description">The new description of the ToDo item.</param>
/// <param name="DueDate">The new due date of the ToDo item.</param>
/// <param name="IsComplete">The new completion status of the ToDo item.</param>
public record UpdateToDoCommand(Guid Id, string? Title, string? Description, DateTime? DueDate, bool? IsComplete) : IRequest<ToDoResponse>
{
}

internal sealed class UpdateToDoCommandHandler(IToDoRepository repository) : IRequestHandler<UpdateToDoCommand, ToDoResponse>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<ToDoResponse> Handle(UpdateToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id) ?? throw new ToDoNotFoundException(request.Id);

        if (request.IsComplete.HasValue)
        {
            entity.SetIsCompleted(request.IsComplete.Value);
        }

        entity.UpdateDetails(request.Title, request.Description, request.DueDate);

        await _repository.UpdateAsync(entity);
        return new ToDoResponse(entity);
    }
}
