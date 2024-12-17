using core.Models;
using System.Threading.Tasks;

namespace server.Repositories;

/// <summary>
/// Provides data access operations for TaskItem entities
/// </summary>
public interface ITaskRepository
{
    /// <summary>
    /// Retrieves all TaskItems from the database
    /// </summary>
    /// <returns>A collection of all TaskItems</returns>
    Task<IEnumerable<TaskItem>> GetAllAsync();

    /// <summary>
    /// Retrieves a specific TaskItem by its ID
    /// </summary>
    /// <param name="id">The unique identifier of the TaskItem</param>
    /// <returns>The TaskItem if found, null otherwise</returns>
    Task<TaskItem?> GetByIdAsync(int id);

    /// <summary>
    /// Creates a new TaskItem in the database
    /// </summary>
    /// <param name="entity">The TaskItem to create</param>
    /// <returns>The created TaskItem with assigned ID</returns>
    Task<TaskItem> CreateAsync(TaskItem entity);

    /// <summary>
    /// Updates an existing TaskItem in the database
    /// </summary>
    /// <param name="entity">The TaskItem with updated values</param>
    Task UpdateAsync(TaskItem entity);

    /// <summary>
    /// Deletes a TaskItem from the database
    /// </summary>
    /// <param name="id">The ID of the TaskItem to delete</param>
    Task DeleteAsync(int id);

    /// <summary>
    /// Retrieves all TaskItems associated with the specified event IDs
    /// </summary>
    /// <param name="eventIds">List of event IDs to filter TaskItems by</param>
    /// <returns>Collection of TaskItems associated with the specified events</returns>
    Task<IEnumerable<TaskItem>> GetTasksByEventIdsAsync(List<int> eventIds);
}
