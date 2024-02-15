namespace dotnetapp.Models
{
public class Referee
{
    public int RefereeID { get; set; }

    [Required(ErrorMessage = "Referee name is required")]
    public string RefereeName { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Number of matches must be a non-negative number")]
    public int NoOfMatches { get; set; }

    // Navigation property representing the events where the referee officiated
    public List<Event> Events { get; set; }
    
}

}