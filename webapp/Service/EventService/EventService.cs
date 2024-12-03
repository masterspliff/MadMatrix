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
}