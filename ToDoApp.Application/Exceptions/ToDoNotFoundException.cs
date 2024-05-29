namespace ToDoApp.Application.Exceptions;

/// <summary>
/// Represents an exception that is thrown when a ToDo entity is not found.
/// </summary>
/// <param name="id">The id of the ToDo entity.</param>
public class ToDoNotFoundException(Guid id) : Exception($"ToDo entity with id {id} was not found.")
{
}
