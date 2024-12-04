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

    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _locations.Find(_ => true).ToListAsync();
    }

    public async Task<Location?> GetByIdAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        return await _locations.Find(filter).FirstOrDefaultAsync();
    }

    public async Task<Location> CreateAsync(Location entity)
    {
        // Get the highest existing ID and increment by 1
        var highestId = await _locations.Find(_ => true)
            .SortByDescending(x => x.Id)
            .Project(x => x.Id)
            .FirstOrDefaultAsync();
        
        entity.Id = highestId + 1;
        
        await _locations.InsertOneAsync(entity);
        return entity;
    }

    public async Task UpdateAsync(Location entity)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, entity.Id);
        await _locations.ReplaceOneAsync(filter, entity);
    }

    public async Task DeleteAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        await _locations.DeleteOneAsync(filter);
    }
}
