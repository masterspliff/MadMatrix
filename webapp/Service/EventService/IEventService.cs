namespace webapp.Service;
using core.Models;

/// <summary>
/// Provides services for managing task events in the system
/// </summary>
public interface IEventService
{
    /// <summary>
    /// Creates a new task event in the system
    /// </summary>
    /// <param name="newEvent">The event to be created</param>
    /// <returns>True if the event was created successfully, false otherwise</returns>
    Task<bool> CreateEventAsync(TaskEvent newEvent);

    /// <summary>
    /// Retrieves all task events from the system
    /// </summary>
    /// <returns>A list of all task events, empty list if no events exist</returns>
    Task<List<TaskEvent>> GetAllEventsAsync();

    /// <summary>
    /// Retrieves a specific task event by its ID
    /// </summary>
    /// <param name="eventId">The unique identifier of the event</param>
    /// <returns>The requested task event</returns>
    /// <exception cref="Exception">Thrown when the event is not found or when the request fails</exception>
    Task<TaskEvent> GetEventByIdAsync(int eventId);

    /// <summary>
    /// Deletes a task event from the system
    /// </summary>
    /// <param name="id">The unique identifier of the event to delete</param>
    /// <returns>True if the event was deleted successfully, false otherwise</returns>
    Task<bool> DeleteEventAsync(int id);

    /// <summary>
    /// Updates an existing task event in the system
    /// </summary>
    /// <param name="id">The unique identifier of the event to update</param>
    /// <param name="taskEvent">The updated event information</param>
    /// <returns>True if the event was updated successfully, false otherwise</returns>
    Task<bool> UpdateEventAsync(int id, TaskEvent taskEvent);
}
