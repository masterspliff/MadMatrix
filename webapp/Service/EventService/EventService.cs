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
        var response = await _httpClient.PostAsJsonAsync("event", newEvent);
        return response.IsSuccessStatusCode;
    }
    public async Task<List<TaskEvent>> GetAllEventsAsync()
    {
        var response = await _httpClient.GetAsync("event");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<TaskEvent>>();
        }
        throw new Exception("Could not fetch events");
    }
    
    public async Task<TaskEvent> GetEventByIdAsync(int eventId)
    {
        var response = await _httpClient.GetAsync($"event/{eventId}");
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<TaskEvent>();
        }
        throw new Exception("Could not fetch event");
    }
}