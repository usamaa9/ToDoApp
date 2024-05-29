namespace ToDoApp.Application.Commands;

/// <summary>
/// Represents a command to delete a ToDo item.
/// </summary>
/// <param name="Id">The unique identifier of the ToDo item to delete.</param>
public record DeleteToDoCommand(Guid Id) : IRequest<bool>;

internal sealed class DeleteToDoCommandHandler(IToDoRepository repository) : IRequestHandler<DeleteToDoCommand, bool>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<bool> Handle(DeleteToDoCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id);
        if (entity == null)
        {
            return false;
        }

        await _repository.DeleteAsync(entity);
        return true;
    }
}
