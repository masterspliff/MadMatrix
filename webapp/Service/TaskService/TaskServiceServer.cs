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
    
    public async Task<List<TaskItem>> LoadTask()
    {
        List<TaskItem> tasks = new List<TaskItem>();
        
            var response = await _httpClient.GetAsync("task");
            if (response.IsSuccessStatusCode)
            {
                tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
                return tasks;
            }
            throw new Exception("Could not load task");
    }
}


