using core.Models;

namespace server.Repositories;

public interface IEventRepository
{
    Task<IEnumerable<TaskEvent>> GetAllAsync();
    Task<TaskEvent?> GetByIdAsync(int id);
    Task<TaskEvent> CreateAsync(TaskEvent entity);
    Task UpdateAsync(TaskEvent entity);
    Task DeleteAsync(int id);
}
