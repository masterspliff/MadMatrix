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

    public async System.Threading.Tasks.Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _tasks.Find(_ => true).ToListAsync();
    }

    public async System.Threading.Tasks.Task<TaskItem?> GetByIdAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        return await _tasks.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<TaskItem> CreateAsync(TaskItem entity)
    {
        await _tasks.InsertOneAsync(entity);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(TaskItem entity)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, entity.Id);
        await _tasks.ReplaceOneAsync(filter, entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var filter = Builders<TaskItem>.Filter.Eq(e => e.Id, id);
        await _tasks.DeleteOneAsync(filter);
    }
}
