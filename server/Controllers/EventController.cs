using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class EventController : ControllerBase
{
    private readonly IEventRepository _eventRepository;

    public EventController(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var events = await _eventRepository.GetAllAsync();
        return Ok(events);
    }

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

    [HttpPost]
    public async Task<IActionResult> Create(TaskEvent taskEvent)
    {
        var createdEvent = await _eventRepository.CreateAsync(taskEvent);
        return CreatedAtAction(nameof(GetById), new { id = createdEvent.Id }, createdEvent);
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _eventRepository.DeleteAsync(id);
        return NoContent();
    }
}
