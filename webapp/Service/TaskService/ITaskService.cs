namespace webapp.Service;
using core.Models;

public interface ITaskService
{
    Task<bool> CreateTaskAsync(TaskItem newTaskItem);
    Task<List<TaskItem>> LoadTask();
}