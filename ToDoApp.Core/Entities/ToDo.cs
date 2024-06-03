namespace ToDoApp.Core.Entities;

/// <summary>
/// Represents a ToDo item.
/// </summary>
/// <param name="id">The unique identifier of the ToDo item.</param>
/// <param name="title">The title of the ToDo item.</param>
/// <param name="description">The description of the ToDo item.</param>
/// <param name="dueDate">The due date of the ToDo item.</param>
/// <param name="isCompleted">A value indicating whether the ToDo item is completed or not.</param>
public class ToDo(Guid id, string title, string description, DateTime dueDate, bool isCompleted)
{
    /// <summary>
    /// Gets the unique identifier of the ToDo item.
    /// </summary>
    public Guid Id { get; } = id;

    /// <summary>
    /// Gets or sets the title of the ToDo item.
    /// </summary>
    public string Title { get; private set; } = title;

    /// <summary>
    /// Gets or sets the description of the ToDo item.
    /// </summary>
    public string Description { get; private set; } = description;

    /// <summary>
    /// Gets or sets the due date of the ToDo item.
    /// </summary>
    public DateTime DueDate { get; private set; } = dueDate;

    /// <summary>
    /// Gets or sets a value indicating whether the ToDo item is completed or not.
    /// </summary>
    public bool IsCompleted { get; private set; } = isCompleted;

    /// <summary>
    /// Marks the ToDo item as completed or not completed.
    /// </summary>
    public ToDo SetIsCompleted(bool status)
    {
        IsCompleted = status;
        return this;
    }

    /// <summary>
    /// Updates the details of the ToDo item.
    /// </summary>
    /// <param name="title">The new title of the ToDo item.</param>
    /// <param name="description">The new description of the ToDo item.</param>
    /// <param name="dueDate">The new due date of the ToDo item.</param>
    public ToDo UpdateDetails(string? title, string? description, DateTime? dueDate)
    {
        Title = title ?? Title;
        Description = description ?? Description;
        DueDate = dueDate ?? DueDate;
        return this;
    }
}
