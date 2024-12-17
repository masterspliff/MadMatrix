using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

/// <summary>
/// Provides data access operations for Location entities
/// </summary>
public interface ILocationRepository
{
    /// <summary>
    /// Retrieves all Locations from the database
    /// </summary>
    /// <returns>A collection of all Locations</returns>
    Task<IEnumerable<Location>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific Location by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the Location</param>
    /// <returns>The Location if found, null otherwise</returns>
    Task<Location?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new Location in the database
    /// </summary>
    /// <param name="entity">The Location to create</param>
    /// <returns>The created Location with assigned ID</returns>
    Task<Location> CreateAsync(Location entity);

    /// <summary>
    /// Updates an existing Location in the database
    /// </summary>
    /// <param name="entity">The Location with updated values</param>
    Task UpdateAsync(Location entity);

    /// <summary>
    /// Deletes a Location from the database
    /// </summary>
    /// <param name="id">The ID of the Location to delete</param>
    Task DeleteAsync(int id);
}
