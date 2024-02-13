namespace dotnetapp.Models
{
    public class CricketTournament
    {
        public int TournamentId { get; set; }
        public int UserId { get; set; }
        public string TournamentName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Location { get; set; }
        public string Prize { get; set; }
        public string Rules { get; set; }
        public string CoverImage { get; set; }
    }
}
