using core.Models;

namespace webapp.Service;

public interface ILocationService
{
    Task<IEnumerable<Location>> GetAllLocationsAsync();

    Task<bool> CreateLocationAsync(Location newLocationItem);
    Task<bool> DeleteLocationAsync(int id);

}
