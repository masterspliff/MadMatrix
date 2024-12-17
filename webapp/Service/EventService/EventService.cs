namespace webapp.Service;
using System.Net.Http.Json;
using core.Models;

/// <summary>
/// Implementation of the IEventService interface that handles task event operations
/// </summary>
public class EventService : IEventService
{
    /// <summary>
    /// HTTP client used for making requests to the event API endpoints
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the EventService
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests</param>
    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    /// <summary>
    /// Creates a new task event by sending a POST request to the API
    /// </summary>
    /// <param name="newEvent">The event object containing all required event information</param>
    /// <returns>True if creation was successful, false if the API request failed</returns>
    /// <exception cref="Exception">Thrown when there's an error during the API request</exception>
    public async Task<bool> CreateEventAsync(TaskEvent newEvent)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("event", newEvent);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error creating event: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Retrieves all events from the system through the API
    /// </summary>
    /// <returns>A list of all TaskEvent objects, or an empty list if none exist</returns>
    /// <exception cref="Exception">Thrown when the API request fails or returns an error</exception>
    public async Task<List<TaskEvent>> GetAllEventsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("event");
            if (response.IsSuccessStatusCode)
            {
                var events = await response.Content.ReadFromJsonAsync<List<TaskEvent>>();
                return events ?? new List<TaskEvent>();
            }
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Failed to fetch events. Status: {response.StatusCode}, Error: {errorContent}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching events: {ex.Message}", ex);
        }
    }
    
    /// <summary>
    /// Retrieves a specific event by its ID from the API
    /// </summary>
    /// <param name="eventId">The unique identifier of the event to retrieve</param>
    /// <returns>The requested TaskEvent object</returns>
    /// <exception cref="Exception">Thrown when the event is not found or the API request fails</exception>
    public async Task<TaskEvent> GetEventByIdAsync(int eventId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"event/{eventId}");
            if (response.IsSuccessStatusCode)
            {
                var eventItem = await response.Content.ReadFromJsonAsync<TaskEvent>();
                return eventItem ?? throw new Exception("Event not found");
            }
            throw new Exception($"Could not fetch event. Status: {response.StatusCode}");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching event: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deletes an event from the system using its ID
    /// </summary>
    /// <param name="id">The unique identifier of the event to delete</param>
    /// <returns>True if deletion was successful, false if the API request failed</returns>
    /// <exception cref="Exception">Thrown when there's an error during the API request</exception>
    public async Task<bool> DeleteEventAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"event/{id}");
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error deleting event: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Updates an existing event in the system
    /// </summary>
    /// <param name="id">The unique identifier of the event to update</param>
    /// <param name="taskEvent">The updated event information</param>
    /// <returns>True if update was successful, false if the API request failed</returns>
    /// <exception cref="Exception">Thrown when there's an error during the API request</exception>
    public async Task<bool> UpdateEventAsync(int id, TaskEvent taskEvent)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"event/{id}", taskEvent);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating event: {ex.Message}", ex);
        }
    }
}
