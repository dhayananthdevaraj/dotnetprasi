namespace dotnetapp.Models
{
public class Team
{
    public int TeamId { get; set; }

    // [Required(ErrorMessage = "Team name is required")]
    public string TeamName { get; set; }

    // [Required(ErrorMessage = "Team owner is required")]
    public string TeamOwner { get; set; }

    // [Required(ErrorMessage = "Established year is required")]
    public int EstablishedYear { get; set; }

    // Navigation property representing the players in the team
    public List<Player> Players { get; set; }
}

}
