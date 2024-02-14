public class Player
{
    public int PlayerId { get; set; }
    public string PlayerName { get; set; }
    public int Age { get; set; }
    public string Country { get; set; }
    public string BattingStyle { get; set; }
    public string BowlingStyle { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Role { get; set; } // Batsman, Bowler, All-Rounder, Wicketkeeper, etc.
    public int TotalMatchesPlayed { get; set; }
    public int TotalRunsScored { get; set; }
    public int TotalWicketsTaken { get; set; }
    public int TotalCatches { get; set; }
  
}
