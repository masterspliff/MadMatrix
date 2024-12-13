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

    public async Task<bool> CreateLocationAsync(Location newLocationItem)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Location", newLocationItem);
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DeleteLocationAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Location/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception)
        {
            return false;
        }
    }
    
    public async Task<Location?> GetLocationByIdAsync(int id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"Location/{id}");
            if (response.IsSuccessStatusCode)
            {
                var location = await response.Content.ReadFromJsonAsync<Location>();
                return location;
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }

    
}
