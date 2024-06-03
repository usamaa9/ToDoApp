using ToDoApp.Core.Entities;

namespace ToDoApp.Core.Repositories;

/// <summary>
/// Repository for managing ToDo items.
/// </summary>
public interface IToDoRepository
{
    /// <summary>
    /// Retrieves a ToDo item by its ID.
    /// </summary>
    /// <param name="id">The ID of the ToDo item.</param>
    /// <returns>The retrieved ToDo item, or null if not found.</returns>
    Task<ToDo?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves a list of all ToDo items.
    /// </summary>
    /// <returns>A list of all ToDo items.</returns>
    Task<IEnumerable<ToDo>> ListAsync();

    /// <summary>
    /// Adds a new ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to add.</param>
    Task AddAsync(ToDo toDo);

    /// <summary>
    /// Updates an existing ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to update.</param>
    Task UpdateAsync(ToDo toDo);

    /// <summary>
    /// Deletes a ToDo item.
    /// </summary>
    /// <param name="toDo">The ToDo item to delete.</param>
    Task DeleteAsync(ToDo toDo);
}
