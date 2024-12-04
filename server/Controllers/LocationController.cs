using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetAll()
    {
        var locations = await _locationRepository.GetAllAsync();
        return Ok(locations);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Location>> GetById(int id)
    {
        var location = await _locationRepository.GetByIdAsync(id);
        if (location == null)
        {
            return NotFound();
        }
        return Ok(location);
    }

    [HttpPost]
    public async Task<ActionResult<Location>> Create(Location location)
    {
        try
        {
            if (string.IsNullOrEmpty(location.Name))
            {
                return BadRequest("Location name is required");
            }

            var createdLocation = await _locationRepository.CreateAsync(location);
            return CreatedAtAction(nameof(GetById), new { id = createdLocation.Id }, createdLocation);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating location: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
            return StatusCode(500, "Internal server error while creating location");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Location location)
    {
        if (id != location.Id)
        {
            return BadRequest();
        }

        await _locationRepository.UpdateAsync(location);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _locationRepository.DeleteAsync(id);
        return NoContent();
    }
}
