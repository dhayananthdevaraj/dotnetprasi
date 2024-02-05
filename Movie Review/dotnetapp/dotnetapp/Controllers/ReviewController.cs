using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [Route("api/reviews")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<IEnumerable<Review>>> GetReviews(int movieId)
        {
            var reviews = await _context.Reviews.Where(r => r.MovieID == movieId).ToListAsync();
            return Ok(reviews);
        }

        [HttpPost("{movieId}")]
        public async Task<ActionResult<Review>> AddReview(int movieId, Review review)
        {
            review.MovieID = movieId;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
