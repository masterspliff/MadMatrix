using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMongoCollection<TaskItem> _tasks;

    public TaskRepository(MongoDbContext context)
    {
        _tasks = context.Tasks;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _tasks.Find(_ => true).ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        return await _tasks.Find(filter).FirstOrDefaultAsync();
    }

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

    public async Task UpdateAsync(TaskItem entity)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, entity.Id);
        await _tasks.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        await _tasks.DeleteOneAsync(filter);
    }
    
    public async Task<IEnumerable<TaskItem>> GetTasksByEventIdsAsync(List<int> eventIds)
    {
        var filter = Builders<TaskItem>.Filter.AnyIn(t => t.EventIds, eventIds);
        return await _tasks.Find(filter).ToListAsync();
    }

}
