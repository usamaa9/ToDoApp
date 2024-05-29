namespace ToDoApp.Application.Commands;

/// <summary>
/// Represents a command to create a new ToDo item.
/// </summary>
/// <param name="Title">The title of the ToDo item.</param>
/// <param name="Description">The description of the ToDo item.</param>
/// <param name="DueDate">The due date of the ToDo item.</param>
public record CreateToDoCommand([Required, StringLength(100)] string Title, [Required, StringLength(500)] string Description, [Required] DateTime DueDate) : IRequest<ToDoResponse>;

internal sealed class CreateToDoCommandHandler(IToDoRepository repository) : IRequestHandler<CreateToDoCommand, ToDoResponse>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<ToDoResponse> Handle(CreateToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = new ToDo(Guid.NewGuid(), request.Title, request.Description, request.DueDate, false);

        await _repository.AddAsync(entity);
        return new ToDoResponse(entity);
    }
}
