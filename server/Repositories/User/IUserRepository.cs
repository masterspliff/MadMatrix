using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface IUserRepository
{
    System.Threading.Tasks.Task<IEnumerable<User>> GetAllAsync();
    System.Threading.Tasks.Task<User?> GetByIdAsync(int id);
    System.Threading.Tasks.Task<User> CreateAsync(User entity);
    System.Threading.Tasks.Task UpdateAsync(User entity);
    System.Threading.Tasks.Task DeleteAsync(int id);
}
