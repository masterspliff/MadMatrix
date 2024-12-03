using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<TaskItem>> GetAllAsync();
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem entity);
    Task UpdateAsync(TaskItem entity);
    Task DeleteAsync(int id);
}
