using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<TaskEvent> _events;

    public EventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }

    public async System.Threading.Tasks.Task<IEnumerable<TaskEvent>> GetAllAsync()
    {
        return await _events.Find(_ => true).ToListAsync();
    }

    public async System.Threading.Tasks.Task<TaskEvent?> GetByIdAsync(int id)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, id);
        return await _events.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<TaskEvent> CreateAsync(TaskEvent entity)
    {
        await _events.InsertOneAsync(entity);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskEvent entity)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, entity.Id);
        await _events.ReplaceOneAsync(filter, entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, id);
        await _events.DeleteOneAsync(filter);
    }
}
