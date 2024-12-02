using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class EventRepository : IEventRepository
{
    private readonly IMongoCollection<Event> _events;

    public EventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }

    public async System.Threading.Tasks.Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _events.Find(_ => true).ToListAsync();
    }

    public async System.Threading.Tasks.Task<Event?> GetByIdAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        return await _events.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<Event> CreateAsync(Event entity)
    {
        await _events.InsertOneAsync(entity);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(Event entity)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, entity.Id);
        await _events.ReplaceOneAsync(filter, entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        await _events.DeleteOneAsync(filter);
    }
}
