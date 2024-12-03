namespace webapp.Service;
using core.Models;

public interface IEventService
{
    public Task<bool> CreateEventAsync(TaskEvent newEvent);

}