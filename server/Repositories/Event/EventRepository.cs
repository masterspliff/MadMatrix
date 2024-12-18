using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

/// <summary>
/// MongoDB implementation of IEventRepository for TaskEvent data access
/// </summary>
public class EventRepository : IEventRepository
{
    /// <summary>
    /// MongoDB collection for storing TaskEvents. This collection maintains all event records
    /// and handles the persistence of TaskEvent entities.
    /// </summary>
    private readonly IMongoCollection<TaskEvent> _events;
    /// <summary>
    /// Initializes a new instance of the EventRepository
    /// </summary>
    /// <param name="context">The MongoDB database context</param>
    public EventRepository(MongoDbContext context)
    {
        _events = context.Events;
    }
    /// <summary>
    /// Retrieves all TaskEvents from the database
    /// </summary>
    /// <returns>A collection of all TaskEvents in the database</returns>
    public async Task<IEnumerable<TaskEvent>> GetAllAsync()
    {
        var filter = Builders<TaskEvent>.Filter.Empty;
        return await _events.Find(filter).ToListAsync();
    }
    /// <summary>
    /// Retrieves a specific TaskEvent by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the TaskEvent to retrieve</param>
    /// <returns>The TaskEvent if found; null if no TaskEvent exists with the specified ID</returns>
    public async Task<TaskEvent?> GetByIdAsync(int id)
    {
        var filterById = Builders<TaskEvent>.Filter.Eq(taskEvent => taskEvent.Id, id);
        return await _events.Find(filterById).FirstOrDefaultAsync();
    }
    /// <summary>
    /// Creates a new TaskEvent with an automatically assigned ID
    /// </summary>
    /// <param name="entity">The TaskEvent to create</param>
    /// <returns>The created TaskEvent with its assigned ID</returns>
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
    /// <summary>
    /// Updates an existing TaskEvent in the database
    /// </summary>
    /// <param name="entity">The TaskEvent entity with updated values to save</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task UpdateAsync(TaskEvent entity)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, entity.Id);
        await _events.ReplaceOneAsync(filter, entity);
    }
    /// <summary>
    /// Deletes a TaskEvent from the database
    /// </summary>
    /// <param name="id">The unique identifier of the TaskEvent to delete</param>
    /// <returns>A task representing the asynchronous operation</returns>
    public async Task DeleteAsync(int id)
    {
        var filter = Builders<TaskEvent>.Filter.Eq(e => e.Id, id);
        await _events.DeleteOneAsync(filter);
    }
}
