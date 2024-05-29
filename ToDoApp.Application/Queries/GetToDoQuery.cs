namespace ToDoApp.Application.Queries;

/// <summary>
/// Represents a query to retrieve a ToDo item by its Id.
/// </summary>
/// <param name="Id">The Id of the ToDo item to retrieve.</param>
public record GetToDoQuery(Guid Id) : IRequest<ToDoResponse?>;

internal sealed class GetToDoQueryHandler(IToDoRepository repository) : IRequestHandler<GetToDoQuery, ToDoResponse?>
{
    private readonly IToDoRepository _repository = repository;

    public async Task<ToDoResponse?> Handle(GetToDoQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetAsync(request.Id);
        return entity == null ? null : new ToDoResponse(entity);
    }
}
