namespace dotnetapp.Models
{public class Event
{
    public int EventId { get; set; }

    [Required(ErrorMessage = "Event name is required")]
    public string EventName { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "Start time is required")]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
    [Required(ErrorMessage = "End time is required")]
    public DateTime EndTime { get; set; }

    // Navigation property representing the venue of the event
    public Venue Venue { get; set; }

    [Required(ErrorMessage = "Main referee is required")]
    public Referee MainReferee { get; set; }

    // Add more attributes as needed

    // Optional attributes
    public string EventImageURL { get; set; }
    public string Description { get; set; }

    // Navigation property representing the teams participating in the event
    public List<Team> Teams { get; set; }
}
}