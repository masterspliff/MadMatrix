using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public class LocalUserRepository : IUserRepository
{
    private readonly FileRepository<User> _repository;

    public LocalUserRepository(FileRepository<User> repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<User> CreateAsync(User entity)
    {
        return await _repository.CreateAsync(entity);
    }

    public async Task UpdateAsync(User entity)
    {
        await _repository.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }
}
