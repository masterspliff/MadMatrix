using Microsoft.AspNetCore.Mvc;
using core.Models;
using server.Repositories;

namespace server.Controllers;

[ApiController]
[Route("[controller]")]
public class LocationController : ControllerBase
{
    /// <summary>
    /// Repository for handling location data persistence
    /// </summary>
    private readonly ILocationRepository _locationRepository;

    public LocationController(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    /// <summary>
    /// Retrieves all locations from the system
    /// </summary>
    /// <returns>A collection of all locations, or an empty collection if none exist</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Location>>> GetAll()
    {
        var locations = await _locationRepository.GetAllAsync();
        return Ok(locations);
    }

    /// <summary>
    /// Retrieves a specific location by its identifier
    /// </summary>
    /// <param name="id">The unique identifier of the location to retrieve</param>
    /// <returns>The location if found, or NotFound if no location matches the given id</returns>
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

    /// <summary>
    /// Creates a new location in the system
    /// </summary>
    /// <param name="location">The location data to create</param>
    /// <returns>The created location with its assigned id, or BadRequest if validation fails</returns>
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

    /// <summary>
    /// Updates an existing location
    /// </summary>
    /// <param name="id">The id of the location to update</param>
    /// <param name="location">The updated location data</param>
    /// <returns>NoContent if successful, BadRequest if id mismatch</returns>
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

    /// <summary>
    /// Deletes a location from the system
    /// </summary>
    /// <param name="id">The id of the location to delete</param>
    /// <returns>NoContent if successful</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _locationRepository.DeleteAsync(id);
        return NoContent();
    }
}
