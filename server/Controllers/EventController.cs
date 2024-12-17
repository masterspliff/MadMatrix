using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    /// <summary>
    /// Repository for handling event data persistence
    /// </summary>
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Retrieves all events from the system
    /// </summary>
    /// <returns>A collection of all events, or an empty collection if none exist</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventRepository.GetAllAsync();
        return Ok(events);
    }

    /// <summary>
    /// Retrieves a specific event by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the event to retrieve</param>
    /// <returns>The event if found, or NotFound if no event matches the given id</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var eventItem = await _eventRepository.GetByIdAsync(id);
        if (eventItem == null)
        {
            return NotFound();
        }
        return Ok(eventItem);
    }

    /// <summary>
    /// Creates a new event in the system
    /// </summary>
    /// <param name="taskEvent">The event data to create</param>
    /// <returns>The created event with its assigned id</returns>
    [HttpPost]
    public async Task<IActionResult> Create(TaskEvent taskEvent)
    {
        var createdEvent = await _eventRepository.CreateAsync(taskEvent);
        return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
    }

    /// <summary>
    /// Updates an existing event
    /// </summary>
    /// <param name="id">The id of the event to update</param>
    /// <param name="taskEvent">The updated event data</param>
    /// <returns>NoContent if successful, BadRequest if id mismatch</returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TaskEvent taskEvent)
    {
        if (id != taskEvent.Id)
        {
            return BadRequest();
        }

        await _eventRepository.UpdateAsync(taskEvent);
        return NoContent();
    }

    /// <summary>
    /// Deletes an event from the system
    /// </summary>
    /// <param name="id">The id of the event to delete</param>
    /// <returns>NoContent if successful</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _eventRepository.DeleteAsync(id);
        return NoContent();
    }
}
