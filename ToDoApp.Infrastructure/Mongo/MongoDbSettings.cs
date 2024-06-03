namespace ToDoApp.Infrastructure.Mongo;

/// <summary>
/// Represents the settings for MongoDB connection and database.
/// </summary>
public class MongoDbSettings
{
    /// <summary>
    /// Gets or sets the connection string for MongoDB.
    /// </summary>
    public string ConnectionString { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the database in MongoDB.
    /// </summary>
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// Gets or sets the name of the collection for ToDos in MongoDB.
    /// </summary>
    public string TodoCollectionName { get; set; } = null!;

    /// <summary>
    /// Gets or sets a value indicating whether to use MongoDB.
    /// </summary>
    public bool Enabled { get; set; } = false;
}
