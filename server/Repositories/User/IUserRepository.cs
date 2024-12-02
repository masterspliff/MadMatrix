using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> CreateAsync(User entity);
    Task UpdateAsync(User entity);
    Task DeleteAsync(int id);
}
