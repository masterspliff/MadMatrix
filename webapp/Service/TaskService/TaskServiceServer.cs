namespace webapp.Service;
using System.Net.Http.Json;
using core.Models;

/// <summary>
/// Implementation of the ITaskService interface that handles task operations
/// </summary>
public class TaskServiceServer : ITaskService
{
    /// <summary>
    /// HTTP client used for making requests to the task API endpoints
    /// </summary>
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Initializes a new instance of the TaskServiceServer
    /// </summary>
    /// <param name="httpClient">The HTTP client to use for API requests</param>
    public TaskServiceServer(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    /// <summary>
    /// Creates a new task by sending a POST request to the API
    /// </summary>
    /// <param name="newTaskItem">The task object containing all required task information</param>
    /// <returns>True if creation was successful, false if the API request failed</returns>
    public async Task<bool> CreateTaskAsync(TaskItem newTaskItem)
    {
        var response = await _httpClient.PostAsJsonAsync("task", newTaskItem);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Updates an existing task by sending a PUT request to the API
    /// </summary>
    /// <param name="id">The unique identifier of the task to update</param>
    /// <param name="updateTaskItem">The updated task information</param>
    /// <returns>True if update was successful, false if the API request failed</returns>
    public async Task<bool> EditTaskAsync(int id, TaskItem updateTaskItem)
    {
        var response = await _httpClient.PutAsJsonAsync($"task/{id}", updateTaskItem);
        return response.IsSuccessStatusCode;
    }

    /// <summary>
    /// Retrieves all tasks from the system through the API
    /// </summary>
    /// <returns>A list of all TaskItem objects, or an empty list if none exist or if the request fails</returns>
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

    /// <summary>
    /// Retrieves a specific task by its ID from the API
    /// </summary>
    /// <param name="id">The unique identifier of the task to retrieve</param>
    /// <returns>The requested TaskItem object</returns>
    /// <exception cref="Exception">Thrown when the task cannot be loaded</exception>
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

    /// <summary>
    /// Retrieves all tasks associated with specific events from the API
    /// </summary>
    /// <param name="eventIds">List of event IDs to fetch tasks for</param>
    /// <returns>A list of tasks associated with the specified events, or an empty list if none found or if the request fails</returns>
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

    /// <summary>
    /// Deletes a task from the system using its ID
    /// </summary>
    /// <param name="id">The unique identifier of the task to delete</param>
    /// <returns>True if deletion was successful, false if the API request failed</returns>
    public async Task<bool> DeleteTaskAsync(int id)
    {
        var response = await _httpClient.DeleteAsync($"task/{id}");
        return response.IsSuccessStatusCode;
    }
    
    /// <summary>
    /// Updates the status of an existing task
    /// </summary>
    /// <param name="taskId">The unique identifier of the task</param>
    /// <param name="status">The new status to set for the task</param>
    /// <returns>True if the status was updated successfully, false if the API request failed</returns>
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

