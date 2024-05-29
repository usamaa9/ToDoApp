namespace ToDoApp.Application.Queries;

/// <summary>
/// Represents a query to retrieve a list of ToDo items.
/// </summary>
public record ListToDosQuery() : IRequest<IEnumerable<ToDoResponse>>;

internal sealed class ListToDosQueryHandler(IToDoRepository repository) : IRequestHandler<ListToDosQuery, IEnumerable<ToDoResponse>>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<IEnumerable<ToDoResponse>> Handle(ListToDosQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.ListAsync();
        return entities.Select(entity => new ToDoResponse(entity));
    }
}
