using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface ITaskRepository
{
    Task<IEnumerable<core.Models.Task>> GetAllAsync();
    Task<core.Models.Task?> GetByIdAsync(int id);
    Task<core.Models.Task> CreateAsync(core.Models.Task entity);
    Task UpdateAsync(core.Models.Task entity);
    Task DeleteAsync(int id);
}
