namespace webapp.Service;
using core.Models;

/// <summary>
/// Provides services for managing tasks in the system
/// </summary>
public interface ITaskService
{
    /// <summary>
    /// Creates a new task in the system
    /// </summary>
    /// <param name="newTaskItem">The task to be created</param>
    /// <returns>True if the task was created successfully, false otherwise</returns>
    Task<bool> CreateTaskAsync(TaskItem newTaskItem);
    
    /// <summary>
    /// Updates an existing task in the system
    /// </summary>
    /// <param name="id">The unique identifier of the task to update</param>
    /// <param name="updateTaskItem">The updated task information</param>
    /// <returns>True if the task was updated successfully, false otherwise</returns>
    Task<bool> EditTaskAsync(int id, TaskItem updateTaskItem);
    
    /// <summary>
    /// Retrieves all tasks from the system
    /// </summary>
    /// <returns>A list of all tasks, empty list if no tasks exist</returns>
    Task<List<TaskItem>> LoadAllTask();

    /// <summary>
    /// Retrieves a specific task by its ID
    /// </summary>
    /// <param name="taskid">The unique identifier of the task</param>
    /// <returns>The requested task if found</returns>
    /// <exception cref="Exception">Thrown when the task cannot be loaded</exception>
    Task<TaskItem> GetTaskAsync(int taskid);
    
    /// <summary>
    /// Retrieves all tasks associated with specific events
    /// </summary>
    /// <param name="eventId">List of event IDs to fetch tasks for</param>
    /// <returns>A list of tasks associated with the specified events, empty list if none found</returns>
    Task<List<TaskItem>> GetTasksByEventIdAsync(List<int> eventId);
    
    /// <summary>
    /// Deletes a task from the system
    /// </summary>
    /// <param name="id">The unique identifier of the task to delete</param>
    /// <returns>True if the task was deleted successfully, false otherwise</returns>
    Task<bool> DeleteTaskAsync(int id);
    
    /// <summary>
    /// Updates the status of an existing task
    /// </summary>
    /// <param name="taskId">The unique identifier of the task</param>
    /// <param name="status">The new status to set for the task</param>
    /// <returns>True if the status was updated successfully, false otherwise</returns>
    Task<bool> UpdateTaskStatus(int taskId, TaskStatus status);
}
