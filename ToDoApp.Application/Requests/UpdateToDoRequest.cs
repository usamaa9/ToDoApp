namespace ToDoApp.Application.Requests;

/// <summary>
/// Represents a request to update a ToDo item.
/// </summary>
/// <param name="Title">The updated title of the ToDo item.</param>
/// <param name="Description">The updated description of the ToDo item.</param>
/// <param name="DueDate">The updated due date of the ToDo item.</param>
/// <param name="IsComplete">The updated completion status of the ToDo item.</param>
public record UpdateToDoRequest(string? Title, string? Description, DateTime? DueDate, bool? IsComplete);
