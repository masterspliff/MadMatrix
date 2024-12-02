using core.Models;
using server.Repositories.Base;

namespace server.Repositories;

public class LocalLocationRepository : ILocationRepository
{
    private readonly LocalRepository<Location> _location;

    public LocalLocationRepository(LocalRepository<Location> location)
    {
        _location = location;
    }

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _location.GetAllAsync();
    }

    public async Task<Location?> GetByIdAsync(int id)
    {
        return await _location.GetByIdAsync(id);
    }

    public async Task<Location> CreateAsync(Location entity)
    {
        return await _location.CreateAsync(entity);
    }

    public async Task UpdateAsync(Location entity)
    {
        await _location.UpdateAsync(entity);
    }

    public async Task DeleteAsync(int id)
    {
        await _location.DeleteAsync(id);
    }
}
