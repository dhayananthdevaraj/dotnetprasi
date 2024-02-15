namespace dotnetapp.Models
{
    public class Schedule
    {
        public int ScheduleId { get; set; }

        // [Required(ErrorMessage = "Match date and time are required")]
        // [DataType(DataType.DateTime)]
        // [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime MatchDateTime { get; set; }

        // [Required(ErrorMessage = "Event is required")]
        public int EventId { get; set; }

        // Foreign key referencing the Event table
        public Event Event { get; set; }

        // [Required(ErrorMessage = "Venue is required")]
        public int VenueId { get; set; }

        // Foreign key referencing the Venue table
        public Venue Venue { get; set; }

        // [Required(ErrorMessage = "Referee is required")]
        public int RefereeId { get; set; }

        // Foreign key referencing the Referee table
        public Referee MainReferee { get; set; }

        // [Required(ErrorMessage = "Team 1 is required")]
        public int Team1Id { get; set; }

        // Foreign key referencing the Team table for Team 1

        // [Required(ErrorMessage = "Team 2 is required")]
        public int Team2Id { get; set; }

        // Foreign key referencing the Team table for Team 2

        // Add more attributes as needed

        // Navigation property representing the players participating in the match
        public List<Player> Players { get; set; }
    }
}
