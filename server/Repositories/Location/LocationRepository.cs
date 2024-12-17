using core.Models;
using MongoDB.Driver;
using server.Data;

namespace server.Repositories;

/// <summary>
/// MongoDB implementation of ILocationRepository for Location data access
/// </summary>
public class LocationRepository : ILocationRepository
{
    /// <summary>
    /// MongoDB collection for storing Location entities
    /// </summary>
    private readonly IMongoCollection<Location> _locations;

    /// <summary>
    /// Initializes a new instance of the LocationRepository
    /// </summary>
    /// <param name="context">The MongoDB database context</param>
    public LocationRepository(MongoDbContext context)
    {
        _locations = context.Locations;
    }

    /// <summary>
    /// Retrieves all Location entities from the database
    /// </summary>
    /// <returns>A collection of all Location entities in the database</returns>
    public async Task<IEnumerable<Location>> GetAllAsync()
    {
        return await _locations.Find(_ => true).ToListAsync();
    }

    /// <summary>
    /// Retrieves a specific Location by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the Location</param>
    /// <returns>The Location if found, null otherwise</returns>
    public async Task<Location?> GetByIdAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        return await _locations.Find(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Creates a new Location in the database with an auto-generated ID
    /// </summary>
    /// <param name="entity">The Location to create</param>
    /// <returns>The created Location with its assigned ID</returns>
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

    /// <summary>
    /// Updates an existing Location in the database
    /// </summary>
    /// <param name="entity">The Location with updated values</param>
    public async Task UpdateAsync(Location entity)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, entity.Id);
        await _locations.ReplaceOneAsync(filter, entity);
    }

    /// <summary>
    /// Deletes a Location from the database
    /// </summary>
    /// <param name="id">The ID of the Location to delete</param>
    public async Task DeleteAsync(int id)
    {
        var filter = Builders<Location>.Filter.Eq(e => e.Id, id);
        await _locations.DeleteOneAsync(filter);
    }
}
