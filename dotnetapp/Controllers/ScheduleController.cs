using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetapp.Models;

namespace dotnetapp.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Schedule>>> GetAllSchedules()
        {
            var schedules = await _context.Schedules.ToListAsync();

            return Ok(schedules);
        }

        [HttpGet("{scheduleId}")]
        public async Task<ActionResult<Schedule>> GetScheduleById(int scheduleId)
        {
            var schedule = await _context.Schedules
                .FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);

            if (schedule == null)
            {
                return NotFound(new { message = "Cannot find any schedule" });
            }

            return Ok(schedule);
        }

        [HttpPost]
        public async Task<ActionResult> AddSchedule([FromBody] Schedule schedule)
        {
            try
            {
                _context.Schedules.Add(schedule);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Schedule added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{scheduleId}")]
        public async Task<ActionResult> UpdateSchedule(int scheduleId, [FromBody] Schedule schedule)
        {
            try
            {
                var existingSchedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);

                if (existingSchedule == null)
                {
                    return NotFound(new { message = "Cannot find any schedule" });
                }

                schedule.ScheduleId = scheduleId;

                _context.Entry(existingSchedule).CurrentValues.SetValues(schedule);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Schedule updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{scheduleId}")]
        public async Task<ActionResult> DeleteSchedule(int scheduleId)
        {
            try
            {
                var schedule = await _context.Schedules.FirstOrDefaultAsync(s => s.ScheduleId == scheduleId);

                if (schedule == null)
                {
                    return NotFound(new { message = "Cannot find any schedule" });
                }

                _context.Schedules.Remove(schedule);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Schedule deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
