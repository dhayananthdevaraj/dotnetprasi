using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/team")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TeamController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> GetAllTeams()
        {
            var teams = await _context.Teams.ToListAsync();

            return Ok(teams);
        }

        [HttpGet("{teamId}")]
        public async Task<ActionResult<Team>> GetTeamById(int teamId)
        {
            var team = await _context.Teams
                .FirstOrDefaultAsync(t => t.TeamId == teamId);

            if (team == null)
            {
                return NotFound(new { message = "Cannot find any team" });
            }

            return Ok(team);
        }

        [HttpPost]
        public async Task<ActionResult> AddTeam([FromBody] Team team)
        {
            try
            {
                _context.Teams.Add(team);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Team added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{teamId}")]
        public async Task<ActionResult> UpdateTeam(int teamId, [FromBody] Team team)
        {
            try
            {
                var existingTeam = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);

                if (existingTeam == null)
                {
                    return NotFound(new { message = "Cannot find any team" });
                }

                team.TeamId = teamId;

                _context.Entry(existingTeam).CurrentValues.SetValues(team);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Team updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{teamId}")]
        public async Task<ActionResult> DeleteTeam(int teamId)
        {
            try
            {
                var team = await _context.Teams.FirstOrDefaultAsync(t => t.TeamId == teamId);

                if (team == null)
                {
                    return NotFound(new { message = "Cannot find any team" });
                }

                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Team deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
