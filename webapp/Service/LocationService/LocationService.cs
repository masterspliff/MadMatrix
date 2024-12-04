using System.Net.Http.Json;
using core.Models;

namespace webapp.Service;

public class LocationService : ILocationService
{
    private readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<Location>> GetAllLocationsAsync()
    {
        try
        {
            var locations = await _httpClient.GetFromJsonAsync<IEnumerable<Location>>("Location");
            return locations ?? Enumerable.Empty<Location>();
        }
        catch (Exception)
        {
            return Enumerable.Empty<Location>();
        }
    }

    public async Task<Location?> GetLocationByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<Location>($"Location/{id}");
        }
        catch (Exception)
        {
            return null;
        }
    }
}
