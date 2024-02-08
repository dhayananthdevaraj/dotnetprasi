using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStoreDBFirst.Models;

namespace BookStoreDBFirst.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly JobApplicationDbContext _context;

        public ApplicationController(JobApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Application>>> GetAllApplications()
        {
            var applications = await _context.Applications.ToListAsync();
            return Ok(applications);
        }

        [HttpPost]
        public async Task<ActionResult> AddApplication(Application application)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return detailed validation errors
            }

            Job job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobTitle == application.JobTitle);

            if (job == null)
            {
                // Job with the specified JobTitle doesn't exist
                return NotFound("Job not found");
            }

            //application.Job.JobID = job.JobID;
            //application.Job.JobTitle = job.JobTitle;
            //application.Job.Department = job.Department;
            //application.Job.Location = job.Location;
            //application.Job.Responsibility = job.Responsibility;
            //application.Job.Qualification = job.Qualification;
            //application.Job.DeadLine = job.DeadLine;
            application.Status = "Pending";

            await _context.Applications.AddAsync(application);
            await _context.SaveChangesAsync();

            return Ok();
        }

        //[HttpPost]
        //public async Task<ActionResult> AddApplication(Application application)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState); // Return detailed validation errors
        //    }
        //    Console.WriteLine(application.JobTitle);
        //    Job job = await _context.Jobs.FirstOrDefaultAsync(j => j.JobTitle == application.JobTitle);
        //    if (job == null)
        //    {
        //        // Job with the specified JobTitle doesn't exist
        //        return NotFound("Job not found");
        //    }
        //    Console.WriteLine($"Job {job.JobTitle}");
        //    application.Job= job;
        //    Console.WriteLine("asdfghj "+application.Job.JobTitle);


        //    application.Status = "Pending";

        //    await _context.Applications.AddAsync(application);
        //    await _context.SaveChangesAsync();
        //    return Ok();
        //}


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteApplication(int id)
        {
            if (id <= 0)
                return BadRequest("Not a valid Application id");

            var application = await _context.Applications.FindAsync(id);
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditApplication(int id, Application updatedApplication)
        {
            if (id <= 0)
                return BadRequest("Not a valid Application id");

            var existingApplication = await _context.Applications.FindAsync(id);

            if (existingApplication == null)
                return NotFound("Application not found");

            // Update the fields of the existing application
            existingApplication.ApplicationName = updatedApplication.ApplicationName;
            existingApplication.ContactNumber = updatedApplication.ContactNumber;
            existingApplication.MailID = updatedApplication.MailID;
            existingApplication.JobTitle = updatedApplication.JobTitle;
            existingApplication.Status = updatedApplication.Status;


            // Save the changes to the database
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
