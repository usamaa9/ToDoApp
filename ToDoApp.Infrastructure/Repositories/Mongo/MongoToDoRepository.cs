using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ToDoApp.Core.Entities;
using ToDoApp.Core.Repositories;

namespace ToDoApp.Infrastructure.Repositories.Mongo;

internal class MongoToDoRepository : IToDoRepository
{
    private readonly IMongoCollection<ToDo> _repository;

    public MongoToDoRepository(IMongoDatabase mongoDatabase, IOptions<MongoDbSettings> options)
    {
        _repository = mongoDatabase.GetCollection<ToDo>(options.Value.TodoCollectionName);
    }

    public async Task AddAsync(ToDo toDo)
    {
        await _repository.InsertOneAsync(toDo);
    }

    public async Task DeleteAsync(ToDo toDo)
    {
        var filter = Builders<ToDo>.Filter.Eq(t => t.Id, toDo.Id);
        await _repository.DeleteOneAsync(filter);
    }

    public async Task<ToDo?> GetAsync(Guid id)
    {
        var filter = Builders<ToDo>.Filter.Eq(t => t.Id, id);
        return await _repository.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<List<ToDo>> ListAsync()
    {
        return await _repository.Find(new BsonDocument()).ToListAsync();
    }

    public async Task UpdateAsync(ToDo toDo)
    {
        var filter = Builders<ToDo>.Filter.Eq(t => t.Id, toDo.Id);
        await _repository.ReplaceOneAsync(filter, toDo);
    }
}
