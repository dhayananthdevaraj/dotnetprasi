namespace dotnetapp.Models
{
  

public class Team
{
    public int TeamId { get; set; }

    [Required(ErrorMessage = "Team name is required")]
    public string TeamName { get; set; }

    [Required(ErrorMessage = "Team owner is required")]
    public string TeamOwner { get; set; }

    [Range(1800, 2100, ErrorMessage = "Established year must be between 1800 and 2100")]
    public int EstablishedYear { get; set; }

    // Navigation property representing the players in the team
    public List<Player> Players { get; set; }
}

}
