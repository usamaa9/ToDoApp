using MongoDB.Bson.Serialization.Attributes;
using ToDoApp.Core.Entities;

namespace ToDoApp.Infrastructure.Mongo.Models;

/// <summary>
/// Represents a ToDo item.
/// </summary>
public class ToDoModel
{
    /// <summary>
    /// the unique identifier of the ToDo item.
    /// </summary>
    [BsonId]
    public Guid Id { get; set; }

    /// <summary>
    /// the title of the ToDo item.
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// the description of the ToDo item.
    /// </summary>
    public string Description { get; set; } = null!;

    /// <summary>
    /// the due date of the ToDo item.
    /// </summary>
    public DateTime DueDate { get; set; }

    /// <summary>
    /// a value indicating whether the ToDo item is completed.
    /// </summary>
    public bool IsCompleted { get; set; }
}

internal static class ToDoModelExtensions
{
    public static ToDo? ToCoreEntity(this ToDoModel? model) =>
        model is null ? null :
        new ToDo(model.Id, model.Title, model.Description, model.DueDate, model.IsCompleted);

    public static ToDoModel? ToMongoModel(this ToDo? entity) =>
        entity is null ? null :
        new ToDoModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            DueDate = entity.DueDate,
            IsCompleted = entity.IsCompleted
        };
}
