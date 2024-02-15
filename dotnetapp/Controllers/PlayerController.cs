using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PlayerController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetAllPlayers()
        {
            var players = await _context.Players.ToListAsync();

            return Ok(players);
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult<Player>> GetPlayerById(int playerId)
        {
            var player = await _context.Players
                .FirstOrDefaultAsync(p => p.PlayerId == playerId);

            if (player == null)
            {
                return NotFound(new { message = "Cannot find any player" });
            }

            return Ok(player);
        }

        [HttpPost]
        public async Task<ActionResult> AddPlayer([FromBody] Player player)
        {
            try
            {
                _context.Players.Add(player);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Player added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{playerId}")]
        public async Task<ActionResult> UpdatePlayer(int playerId, [FromBody] Player player)
        {
            try
            {
                var existingPlayer = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);

                if (existingPlayer == null)
                {
                    return NotFound(new { message = "Cannot find any player" });
                }

                player.PlayerId = playerId;

                _context.Entry(existingPlayer).CurrentValues.SetValues(player);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Player updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{playerId}")]
        public async Task<ActionResult> DeletePlayer(int playerId)
        {
            try
            {
                var player = await _context.Players.FirstOrDefaultAsync(p => p.PlayerId == playerId);

                if (player == null)
                {
                    return NotFound(new { message = "Cannot find any player" });
                }

                _context.Players.Remove(player);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Player deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
