using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class LocalEventRepository : IEventRepository
{
    private readonly IMongoCollection<Event> _events;

    public LocalEventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }

    public async Task<IEnumerable<Event>> GetAllAsync()
    {
        return await _events.Find(_ => true).ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        return await _events.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Event> CreateAsync(Event entity)
    {
        await _events.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(Event entity)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, entity.Id);
        await _events.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<Event>.Filter.Eq(e => e.Id, id);
        await _events.DeleteOneAsync(filter);
    }
}
