namespace core.Models;

public class Event
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string LocationId { get; set; }
    public List<string> TaskIds { get; set; } = new();
    public string Customer { get; set; }
    public string ChooseFood { get; set; }
    public string SpecialRequest { get; set; }
    public int Participant { get; set; } = new();
    
    public bool IsCompleted { get; set; }
}
