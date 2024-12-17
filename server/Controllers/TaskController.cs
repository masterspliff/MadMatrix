using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;
using MongoDB.Driver;

using webapp.Service;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    /// <summary>
    /// Repository for handling task data persistence
    /// </summary>
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository) // navngivning??
    {
        _taskRepository = taskRepository;
    }

    /// <summary>
    /// Retrieves all tasks from the system
    /// </summary>
    /// <returns>A collection of all tasks, or an empty collection if none exist</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return Ok(tasks);
    }

    /// <summary>
    /// Retrieves a specific task by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the task to retrieve</param>
    /// <returns>The task if found, or NotFound if no task matches the given id</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound();  
        }
        return Ok(task);
    }

    /// <summary>
    /// Creates a new task in the system
    /// </summary>
    /// <param name="task">The task data to create, must include at least one assigned user</param>
    /// <returns>The created task with its assigned id, or BadRequest if validation fails</returns>
    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        // Check if task has a list of users to assign
        if (task.AssignedToIds == null || !task.AssignedToIds.Any())
        {
            return BadRequest("At least one user must be assigned to the task.");
        }

        var createdTask = await _taskRepository.CreateAsync(task);

        // Optionally, send the assigned user IDs back in the response
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }


    /// <summary>
    /// Updates an existing task
    /// </summary>
    /// <param name="id">The id of the task to update</param>
    /// <param name="task">The updated task data</param>
    /// <returns>NoContent if successful, BadRequest if validation fails</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskItem task)
    {
        if (id != task.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _taskRepository.UpdateAsync(task);
        return NoContent();
    }

    /// <summary>
    /// Deletes a task from the system
    /// </summary>
    /// <param name="id">The id of the task to delete</param>
    /// <returns>NoContent if successful, NotFound if task doesn't exist</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
        {
            return NotFound();
        }

        await _taskRepository.DeleteAsync(id);
        return NoContent();
    }
    
    /// <summary>
    /// Retrieves all tasks associated with specific events
    /// </summary>
    /// <param name="eventIds">Comma-separated list of event IDs</param>
    /// <returns>Collection of tasks associated with the specified events, or BadRequest if invalid input</returns>
    [HttpGet("byevents/{eventIds}")]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasksByEventIds(string eventIds)
    {
        try
        {
            var eventIdList = eventIds.Split(',').Select(int.Parse).ToList();
            var tasks = await _taskRepository.GetTasksByEventIdsAsync(eventIdList);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return BadRequest($"Error getting tasks: {ex.Message}");
        }
    }
    
    
    



}
