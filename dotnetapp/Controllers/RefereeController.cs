using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/referee")]
    [ApiController]
    public class RefereeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public RefereeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Referee>>> GetAllReferees()
        {
            var referees = await _context.Referees.ToListAsync();

            return Ok(referees);
        }

        [HttpGet("{refereeId}")]
        public async Task<ActionResult<Referee>> GetRefereeById(int refereeId)
        {
            var referee = await _context.Referees
                .FirstOrDefaultAsync(r => r.RefereeID == refereeId);

            if (referee == null)
            {
                return NotFound(new { message = "Cannot find any referee" });
            }

            return Ok(referee);
        }

        [HttpPost]
        public async Task<ActionResult> AddReferee([FromBody] Referee referee)
        {
            try
            {
                _context.Referees.Add(referee);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Referee added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{refereeId}")]
        public async Task<ActionResult> UpdateReferee(int refereeId, [FromBody] Referee referee)
        {
            try
            {
                var existingReferee = await _context.Referees.FirstOrDefaultAsync(r => r.RefereeID == refereeId);

                if (existingReferee == null)
                {
                    return NotFound(new { message = "Cannot find any referee" });
                }

                referee.RefereeID = refereeId;

                _context.Entry(existingReferee).CurrentValues.SetValues(referee);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Referee updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{refereeId}")]
        public async Task<ActionResult> DeleteReferee(int refereeId)
        {
            try
            {
                var referee = await _context.Referees.FirstOrDefaultAsync(r => r.RefereeID == refereeId);

                if (referee == null)
                {
                    return NotFound(new { message = "Cannot find any referee" });
                }

                _context.Referees.Remove(referee);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Referee deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
