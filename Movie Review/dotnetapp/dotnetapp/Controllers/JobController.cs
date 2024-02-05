using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetApp.Models; // Assuming your models are in the DotNetApp.Models namespace

[Route("api/[controller]")]
[ApiController]
public class JobsController : ControllerBase
{
    private readonly YourDbContext _context; // Replace YourDbContext with your actual DbContext class

    public JobsController(YourDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs([FromQuery] int sortValue = 1, [FromQuery] string searchValue = "")
    {
        var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        var jobs = await _context.Jobs
            .Where(job => searchRegex.IsMatch(job.Title))
            .OrderBy(job => job.StartDate)
            .ToListAsync();

        return Ok(jobs);
    }

    [HttpGet("{jobId}")]
    public async Task<ActionResult<Job>> GetJobById(string jobId)
    {
        var job = await _context.Jobs
            .FirstOrDefaultAsync(job => job.JobId == jobId);

        if (job == null)
        {
            return NotFound(new { message = "Cannot find any job" });
        }

        return Ok(job);
    }

    [HttpPost]
    public async Task<ActionResult> AddJob([FromBody] Job job)
    {
        try
        {
            _context.Jobs.Add(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job added successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpPut("{jobId}")]
    public async Task<ActionResult> UpdateJob(string jobId, [FromBody] Job job)
    {
        try
        {
            var existingJob = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);

            if (existingJob == null)
            {
                return NotFound(new { message = "Cannot find any job" });
            }

            _context.Entry(existingJob).CurrentValues.SetValues(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job updated successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpDelete("{jobId}")]
    public async Task<ActionResult> DeleteJob(string jobId)
    {
        try
        {
            var job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobId == jobId);

            if (job == null)
            {
                return NotFound(new { message = "Cannot find any job" });
            }

            _context.Jobs.Remove(job);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Job deleted successfully" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = ex.Message });
        }
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<IEnumerable<Job>>> GetJobsByUserId(string userId, [FromQuery] string searchValue = "")
    {
        var searchRegex = new System.Text.RegularExpressions.Regex(searchValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

        var jobs = await _context.Jobs
            .Where(job => job.UserId == userId && searchRegex.IsMatch(job.Title))
            .ToListAsync();

        return Ok(jobs);
    }
}
