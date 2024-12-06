namespace webapp.Service;
using core.Models;

public interface IEventService
{
    public Task<bool> CreateEventAsync(TaskEvent newEvent);
    Task<List<TaskEvent>> GetAllEventsAsync();
    
    Task<TaskEvent> GetEventByIdAsync(int eventId);

}
