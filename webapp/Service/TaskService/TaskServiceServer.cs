namespace webapp.Service;
using System.Net.Http.Json;
using core.Models;


public class TaskServiceServer : ITaskService
{
    private readonly HttpClient _httpClient;

    public TaskServiceServer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<bool> CreateTaskAsync(TaskItem newTaskItem)
    {
        var response = await _httpClient.PostAsJsonAsync("task", newTaskItem );
        return response.IsSuccessStatusCode;
    }
}

