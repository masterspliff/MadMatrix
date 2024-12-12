namespace webapp.Service;
using System.Net.Http.Json;
using core.Models;

public class EventService : IEventService
{
    private readonly HttpClient _httpClient;

    public EventService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
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
