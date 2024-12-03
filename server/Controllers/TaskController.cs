using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(ITaskRepository taskRepository) // navngivning??
    {
        _taskRepository = taskRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        var tasks = await _taskRepository.GetAllAsync();
        return Ok(tasks);
    }

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

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdTask = await _taskRepository.CreateAsync(task);
        return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
    }

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
}
// update function