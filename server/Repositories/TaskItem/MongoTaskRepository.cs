using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public class MongoTaskRepository : ITaskRepository
{
    private readonly MongoRepository<TaskItem> _task;

    public MongoTaskRepository(MongoRepository<TaskItem> task)
    {
        _task = task;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _task.GetAllAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _task.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem entity)
    {
        return await _task.CreateAsync(entity);
    }

    public async Task UpdateAsync(TaskItem entity)
    {
        await _task.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _task.DeleteAsync(id);
    }
}
