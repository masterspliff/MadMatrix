namespace webapp.Service;
using core.Models;

public interface ITaskService
{
    public Task<bool> CreateTaskAsync(TaskItem newTaskItem);
}