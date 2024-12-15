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
        var response = await _httpClient.PostAsJsonAsync("task", newTaskItem);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> EditTaskAsync(int id, TaskItem updateTaskItem)
    {
        var response = await _httpClient.PutAsJsonAsync($"task/{id}", updateTaskItem);
        return response.IsSuccessStatusCode;
    }

    public async Task<List<TaskItem>> LoadAllTask()
    {
        try
        {
            var response = await _httpClient.GetAsync("task");
            response.EnsureSuccessStatusCode();
            var tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
            return tasks ?? new List<TaskItem>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading tasks: {ex.Message}");
            return new List<TaskItem>();
        }
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

    public async Task<List<TaskItem>> GetTasksByEventIdAsync(List<int> eventIds)
    {
        try
        {
            var eventIdsParam = string.Join(",", eventIds);
            var response = await _httpClient.GetAsync($"task/byevents/{eventIdsParam}");
            if (response.IsSuccessStatusCode)
            {
                var tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
                return tasks ?? new List<TaskItem>();
            }

            throw new Exception("Could not load event tasks");
        }
        catch (Exception ex)
        {
            {
                Console.WriteLine($"Error loading tasks for events: {ex.Message}");
                return new List<TaskItem>();
            }
        }
    }
    public async Task<List<TaskItem>> GetTasksByAssignedToId(List<int> AssignedToIds)
    {
        try
        {
            var userIdParam = string.Join(",", AssignedToIds);
            var response = await _httpClient.GetAsync($"task/byUserId/{userIdParam}");
            if (response.IsSuccessStatusCode)
            {
                var tasks = await response.Content.ReadFromJsonAsync<List<TaskItem>>();
                return tasks ?? new List<TaskItem>();
            }

            throw new Exception("Could not load tasks by AssignedToId");
        }
        catch (Exception ex)
        {
            {
                Console.WriteLine($"Error loading tasks for events: {ex.Message}");
                return new List<TaskItem>();
            }
        }
    }
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"task/{id}");
        return response.IsSuccessStatusCode;
    }
    
    
    public async Task<bool> UpdateTaskStatus(int taskId, TaskStatus status)
    {
        try 
        {
            var task = await GetTaskAsync(taskId);
            task.Status = status;
            var response = await _httpClient.PutAsJsonAsync($"task/{taskId}", task);
            return response.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating task status: {ex.Message}");
            return false;
        }
    }

}

