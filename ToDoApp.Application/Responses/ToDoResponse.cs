namespace ToDoApp.Application.Responses;

/// <summary>
/// Represents a response object for a ToDo item.
/// </summary>
/// <remarks>Initializes a new instance of the <see cref="ToDoResponse"/> class.</remarks>
/// <param name="entity">The ToDo entity.</param>
public class ToDoResponse(ToDo entity)
{
    /// <summary>
    /// Gets the unique identifier of the ToDo item.
    /// </summary>
    public Guid Id { get; } = entity.Id;

    /// <summary>
    /// Gets the title of the ToDo item.
    /// </summary>
    public string Title { get; } = entity.Title;

    /// <summary>
    /// Gets the description of the ToDo item.
    /// </summary>
    public string Description { get; } = entity.Description;

    /// <summary>
    /// Gets the due date of the ToDo item.
    /// </summary>
    public DateTime DueDate { get; } = entity.DueDate;

    /// <summary>
    /// Gets a value indicating whether the ToDo item is completed or not.
    /// </summary>
    public bool IsCompleted { get; } = entity.IsCompleted;
}
