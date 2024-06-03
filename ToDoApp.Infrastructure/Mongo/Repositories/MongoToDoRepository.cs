using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Repositories;
using ToDoApp.Infrastructure.Mongo.Models;

namespace ToDoApp.Infrastructure.Mongo.Repositories;

internal class MongoToDoRepository(IMongoDatabase mongoDatabase, IOptions<MongoDbSettings> options) : IToDoRepository
{
    private readonly IMongoCollection<ToDoModel> _repository = mongoDatabase.GetCollection<ToDoModel>(options.Value.TodoCollectionName);

    public async Task AddAsync(ToDo toDo)
    {
        await _repository.InsertOneAsync(toDo.ToMongoModel()!);
    }

    public async Task DeleteAsync(ToDo toDo)
    {
        var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, toDo.Id);
        await _repository.DeleteOneAsync(filter);
    }

    public async Task<ToDo?> GetAsync(Guid id)
    {
        var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, id);
        return (await _repository.Find(filter).FirstOrDefaultAsync()).ToCoreEntity();
    }

    public async Task<IEnumerable<ToDo>> ListAsync()
    {
        var models = await _repository.Find(new BsonDocument()).ToListAsync();

        if (models is null)
        {
            return [];
        }

        return models.Select(m => m.ToCoreEntity()!);
    }

    public async Task UpdateAsync(ToDo toDo)
    {
        var filter = Builders<ToDoModel>.Filter.Eq(t => t.Id, toDo.Id);
        await _repository.ReplaceOneAsync(filter, toDo.ToMongoModel()!);
    }
}
