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
    
    public async Task<bool> EditTaskAsync(int id, TaskItem updateTaskItem)
    {
        var response = await _httpClient.PutAsJsonAsync($"task/{id}", updateTaskItem);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<TaskItem>> LoadAllTask()
    {
        List<TaskItem> tasks = new List<TaskItem>();
        
            var response = await _httpClient.GetAsync("task");
            if (response.IsSuccessStatusCode)
            {
                tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
                return tasks;
            }
            throw new Exception("Could not load tasks");
    }
    
    public async Task<TaskItem> GetTaskAsync(int id)
    {
        TaskItem task = new TaskItem();
        var response = await _httpClient.GetAsync($"task/{id}");
        if (response.IsSuccessStatusCode)
        {
            task = await response.Content.ReadFromJsonAsync<TaskItem>();
            return task;
        }
        throw new Exception("Could not load task");
    }
}


