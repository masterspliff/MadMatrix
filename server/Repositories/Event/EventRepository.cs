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

    public async Task<IEnumerable<TaskEvent>> GetAllAsync()
    {
        var filter = Builders<TaskEvent>.Filter.Empty;
        return await _events.Find(filter).ToListAsync();
    }

    public async Task<TaskEvent?> GetByIdAsync(int id)
    {
        var filterById = Builders<TaskEvent>.Filter.Eq(taskEvent => taskEvent.Id, id);
        return await _events.Find(filterById).FirstOrDefaultAsync();
    }
    
    public async Task<TaskEvent> CreateAsync(TaskEvent entity)
    {
        // Get the highest existing ID and add 1, or start at 1 if no events exist
        var lastEvent = await _events.Find(_ => true)
            .SortByDescending(e => e.Id) 
            .FirstOrDefaultAsync(); 

       
        entity.Id = (lastEvent?.Id ?? 0) + 1;

        
        await _events.InsertOneAsync(entity);

        return entity;
    }

    public async Task UpdateAsync(TaskEvent entity)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, entity.Id);
        await _events.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, id);
        await _events.DeleteOneAsync(filter);
    }
}
