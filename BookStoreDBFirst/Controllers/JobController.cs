using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;
using Microsoft.AspNetCore.Authorization;

namespace BookStoreDBFirst.Controllers
{
    //[Authorize(Roles = "admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly JobApplicationDbContext _context;

        public JobController(JobApplicationDbContext context)
        {
            _context = context;
        }
        [HttpPut("{id}")]
public async Task<IActionResult> EditJob(int id, Job updatedJob)
{
    if (id <= 0)
        return BadRequest("Not a valid Job id");

    var existingJob = await _context.Jobs.FindAsync(id);

    if (existingJob == null)
        return NotFound("Job not found");

    // Update the fields of the existing job
    existingJob.JobTitle = updatedJob.JobTitle;
    existingJob.Department = updatedJob.Department;
    existingJob.Location = updatedJob.Location;
    existingJob.Responsibility = updatedJob.Responsibility;
    existingJob.Qualification = updatedJob.Qualification;
    existingJob.DeadLine = updatedJob.DeadLine;

    // Save the changes to the database
    await _context.SaveChangesAsync();

    return NoContent();
}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Job>>> GetAllJobs()
        {
            var jobs = await _context.Jobs.ToListAsync();
            return Ok(jobs);
        }
[HttpGet("JobTitle")]
public async Task<ActionResult<IEnumerable<string>>> Get()
{
    // Project the JobTitle property using Select
    var jobTitles = await _context.Jobs
        .OrderBy(x => x.JobTitle)
        .Select(x => x.JobTitle)
        .ToListAsync();

    return jobTitles;
}
        [HttpPost]
        public async Task<ActionResult> AddJob(Job job)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Job id");

            var job = await _context.Jobs.FindAsync(id);
              _context.Jobs.Remove(job);
                await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
