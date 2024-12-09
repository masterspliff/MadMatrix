using core.Models;

namespace webapp.Service;

public class TaskServiceInMemory : ITaskService
{
    private readonly List<TaskItem> _tasks = new();

    public Task<bool> CreateTaskAsync(TaskItem newTaskItem)
    {
        _tasks.Add(newTaskItem);
        return Task.FromResult(true);
    }

    public Task<List<TaskItem>> LoadTask()
    {
        return Task.FromResult(_tasks);
    }

    public Task<List<TaskItem>> GetTasksByEventIdAsync(int eventId)
    {
        var tasksByEventId = _tasks.Where(task => task.EventId == eventId).ToList();
        return Task.FromResult(tasksByEventId);
    }
}
