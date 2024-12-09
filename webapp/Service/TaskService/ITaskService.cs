namespace webapp.Service;
using core.Models;

public interface ITaskService
{
    // Create a new Task
    Task<bool> CreateTaskAsync(TaskItem newTaskItem);
    Task<List<TaskItem>> LoadTask();
    Task<List<TaskItem>> GetTasksByEventIdAsync(int eventId);

    
    // Update existing Task
    Task<bool> EditTaskAsync(int id, TaskItem updateTaskItem);
    
    // Get all Task
    Task<List<TaskItem>> LoadAllTask();

    // Get Task based on id
    Task<TaskItem> GetTaskAsync(int taskid);

}