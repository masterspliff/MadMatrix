namespace core.Models;

public class Location
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public int? eventId { get; set; }
}