namespace core.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DateForEvent { get; set; }
    public string LocationId { get; set; }
    public List<string> TaskIds { get; set; } = new();
    public string ByCustomer { get; set; }
    public string FoodChoices { get; set; }
    public string SpecialRequest { get; set; }
    public int Participants { get; set; } = new();
}
