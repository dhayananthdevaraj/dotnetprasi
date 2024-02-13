using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

[Route("api/tournaments")]
[ApiController]
public class CricketTournamentController : ControllerBase
{
    private readonly ApplicationDbContext _context; // Replace YourDbContext with your actual DbContext class

    public CricketTournamentController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CricketTournament>>> GetAllCricketTournaments([FromQuery] int? sortValue = 1, [FromQuery] string? searchValue = "")
    {
        var tournaments = await _context.CricketTournaments
            .ToListAsync();

        if (!string.IsNullOrEmpty(searchValue))
        {
            var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

            tournaments = tournaments
                .Where(tournament => searchRegex.IsMatch(tournament.TournamentName)).ToList();
        }

        if (sortValue == -1)
        {
            tournaments = tournaments.OrderByDescending(tournament => tournament.StartDate).ToList();
        }
        else
        {
            tournaments = tournaments.OrderBy(tournament => tournament.StartDate).ToList();
        }

        return Ok(tournaments);
    }

    [HttpGet("{tournamentId}")]
    public async Task<ActionResult<CricketTournament>> GetCricketTournamentById(int tournamentId)
    {
        var tournament = await _context.CricketTournaments
            .FirstOrDefaultAsync(tournament => tournament.TournamentId == tournamentId);

        if (tournament == null)
        {
            return NotFound(new { message = "Cannot find any cricket tournament" });
        }

        return Ok(tournament);
    }

    [HttpPost]
    public async Task<ActionResult> AddCricketTournament([FromBody] CricketTournament tournament)
    {
        try
        {
            _context.CricketTournaments.Add(tournament);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cricket tournament added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("{tournamentId}")]
    public async Task<ActionResult> UpdateCricketTournament(int tournamentId, [FromBody] CricketTournament tournament)
    {
        try
        {
            var existingTournament = await _context.CricketTournaments.FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (existingTournament == null)
            {
                return NotFound(new { message = "Cannot find any cricket tournament" });
            }

            tournament.TournamentId = tournamentId;

            _context.Entry(existingTournament).CurrentValues.SetValues(tournament);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cricket tournament updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{tournamentId}")]
    public async Task<ActionResult> DeleteCricketTournament(int tournamentId)
    {
        try
        {
            var tournament = await _context.CricketTournaments.FirstOrDefaultAsync(t => t.TournamentId == tournamentId);

            if (tournament == null)
            {
                return NotFound(new { message = "Cannot find any cricket tournament" });
            }

            _context.CricketTournaments.Remove(tournament);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Cricket tournament deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }


    [HttpGet("user/{userId}")]
public async Task<ActionResult<IEnumerable<CricketTournament>>> GetTournamentsByUserId(int userId, [FromQuery] string? searchValue = null)
{
    var tournaments = await _context.CricketTournaments
        .Where(tournament => tournament.UserId == userId)
        .ToListAsync();

    if (!string.IsNullOrEmpty(searchValue))
    {
        var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        tournaments = tournaments.Where(tournament => searchRegex.IsMatch(tournament.TournamentName)).ToList();
    }

    return Ok(tournaments);
}


}
