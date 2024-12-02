using Microsoft.AspNetCore.Mvc;
using System;
using core.Models;
using Microsoft.Extensions.Logging;
using server.Repositories;
using server.Repositories.Base;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly ILogger<TaskController> _logger;

    public TaskController(RepositoryFactory repositoryFactory, ILogger<TaskController> logger)
    {                                                                                                                                                                                                         
        _taskRepository = repositoryFactory.CreateTaskRepository();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        try
        {
            var tasks = await _taskRepository.GetAllAsync();
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all tasks.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching task with ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create(TaskItem task)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdTask = await _taskRepository.CreateAsync(task);
            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while creating a new task.");
            return StatusCode(500, "Internal server error");
        }
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

        try
        {
            await _taskRepository.UpdateAsync(task);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while updating task with ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            await _taskRepository.DeleteAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting task with ID {id}.");
            return StatusCode(500, "Internal server error");
        }
    }
}
