using System.Net.Http.Json;
using core.Models;

namespace webapp.Service;

/// <summary>
/// Implementation of the ILocationService interface that handles location operations
/// </summary>
public class LocationService : ILocationService
{
    /// <summary>
    /// HTTP client used for making requests to the location API endpoints
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the LocationService
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests</param>
    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Retrieves all locations from the system through the API
    /// </summary>
    /// <returns>A collection of all Location objects, or an empty collection if none exist or if the request fails</returns>
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

    /// <summary>
    /// Creates a new location by sending a POST request to the API
    /// </summary>
    /// <param name="newLocationItem">The location object containing all required location information</param>
    /// <returns>True if creation was successful, false if the API request failed</returns>
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

    /// <summary>
    /// Deletes a location from the system using its ID
    /// </summary>
    /// <param name="id">The unique identifier of the location to delete</param>
    /// <returns>True if deletion was successful, false if the API request failed</returns>
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
    
    /// <summary>
    /// Retrieves a specific location by its ID from the API
    /// </summary>
    /// <param name="id">The unique identifier of the location to retrieve</param>
    /// <returns>The requested Location object if found, null otherwise</returns>
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
