using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

/// <summary>
/// MongoDB implementation of ITaskRepository for TaskItem data access
/// </summary>
public class TaskRepository : ITaskRepository
{
    /// <summary>
    /// MongoDB collection for storing TaskItem entities
    /// </summary>
    private readonly IMongoCollection<TaskItem> _tasks;

    /// <summary>
    /// Initializes a new instance of the TaskRepository
    /// </summary>
    /// <param name="context">The MongoDB database context</param>
    public TaskRepository(MongoDbContext context)
    {
        _tasks = context.Tasks;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _tasks.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific TaskItem by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the TaskItem</param>
    /// <returns>The TaskItem if found, null otherwise</returns>
    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        return await _tasks.Find(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new TaskItem in the database with an auto-generated ID
    /// </summary>
    /// <param name="entity">The TaskItem to create</param>
    /// <returns>The created TaskItem with its assigned ID</returns>
    public async Task<TaskItem> CreateAsync(TaskItem entity)
    {
        // Get the highest existing ID and add 1, or start at 1 if no events exist
        var lastTaskItem = await _tasks.Find(_ => true)
            .SortByDescending(e => e.Id) 
            .FirstOrDefaultAsync();
        entity.Id = (lastTaskItem?.Id ?? 0) + 1;

        
        await _tasks.InsertOneAsync(entity);

        return entity;
    }

    /// <summary>
    /// Updates an existing TaskItem in the database
    /// </summary>
    /// <param name="entity">The TaskItem with updated values</param>
    public async Task UpdateAsync(TaskItem entity)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, entity.Id);
        await _tasks.ReplaceOneAsync(filter, entity);
    }

    /// <summary>
    /// Deletes a TaskItem from the database
    /// </summary>
    /// <param name="id">The ID of the TaskItem to delete</param>
    public async Task DeleteAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        await _tasks.DeleteOneAsync(filter);
    }
    
    /// <summary>
    /// Retrieves all TaskItems associated with the specified event IDs
    /// </summary>
    /// <param name="eventIds">List of event IDs to filter TaskItems by</param>
    /// <returns>Collection of TaskItems associated with the specified events</returns>
    public async Task<IEnumerable<TaskItem>> GetTasksByEventIdsAsync(List<int> eventIds)
    {
        var filter = Builders<TaskItem>.Filter.AnyIn(t => t.EventIds, eventIds);
        return await _tasks.Find(filter).ToListAsync();
    }

}
