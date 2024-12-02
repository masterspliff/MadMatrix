using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly IMongoCollection<Location> _locations;

    public LocationRepository(MongoDbContext context)
    {
        _locations = context.Locations;
    }

    public async System.Threading.Tasks.Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _locations.Find(_ => true).ToListAsync();
    }

    public async System.Threading.Tasks.Task<Location?> GetByIdAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        return await _locations.Find(filter).FirstOrDefaultAsync();
    }

    public async System.Threading.Tasks.Task<Location> CreateAsync(Location entity)
    {
        await _locations.InsertOneAsync(entity);
        return entity;
    }

    public async System.Threading.Tasks.Task UpdateAsync(Location entity)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, entity.Id);
        await _locations.ReplaceOneAsync(filter, entity);
    }

    public async System.Threading.Tasks.Task DeleteAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        await _locations.DeleteOneAsync(filter);
    }
}
