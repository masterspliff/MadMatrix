using core.Models;

namespace webapp.Service;

/// <summary>
/// Provides services for managing locations in the system
/// </summary>
public interface ILocationService
{
    /// <summary>
    /// Retrieves all locations from the system
    /// </summary>
    /// <returns>A collection of all locations, empty collection if no locations exist</returns>
    Task<IEnumerable<Location>> GetAllLocationsAsync();

    /// <summary>
    /// Creates a new location in the system
    /// </summary>
    /// <param name="newLocationItem">The location to be created</param>
    /// <returns>True if the location was created successfully, false otherwise</returns>
    Task<bool> CreateLocationAsync(Location newLocationItem);

    /// <summary>
    /// Deletes a location from the system
    /// </summary>
    /// <param name="id">The unique identifier of the location to delete</param>
    /// <returns>True if the location was deleted successfully, false otherwise</returns>
    Task<bool> DeleteLocationAsync(int id);

    /// <summary>
    /// Retrieves a specific location by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the location</param>
    /// <returns>The requested location if found, null otherwise</returns>
    Task<Location?> GetLocationByIdAsync(int id);


}
