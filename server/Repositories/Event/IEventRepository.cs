using core.Models;

namespace server.Repositories;

/// <summary>
/// Provides data access operations for TaskEvents
/// </summary>
public interface IEventRepository
{
    /// <summary>
    /// Retrieves all TaskEvents from the database
    /// </summary>
    /// <returns>A collection of all TaskEvents</returns>
    Task<IEnumerable<TaskEvent>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific TaskEvent by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the TaskEvent</param>
    /// <returns>The TaskEvent if found, null otherwise</returns>
    Task<TaskEvent?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new TaskEvent in the database
    /// </summary>
    /// <param name="entity">The TaskEvent to create</param>
    /// <returns>The created TaskEvent with assigned ID</returns>
    Task<TaskEvent> CreateAsync(TaskEvent entity);

    /// <summary>
    /// Updates an existing TaskEvent in the database
    /// </summary>
    /// <param name="entity">The TaskEvent with updated values</param>
    Task UpdateAsync(TaskEvent entity);

    /// <summary>
    /// Deletes a TaskEvent from the database
    /// </summary>
    /// <param name="id">The ID of the TaskEvent to delete</param>
    Task DeleteAsync(int id);
}
