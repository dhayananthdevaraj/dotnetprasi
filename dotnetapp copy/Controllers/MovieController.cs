using System.Linq;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MovieController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {

            var movies = await _context.Movies.ToListAsync();

            foreach (var movie in movies)
            {
                var rev = await _context.Reviews.Where(m => m.MovieID == movie.MovieID).ToListAsync();
                movie.Reviews = rev;
            }

            return Ok(movies);
        }

        [HttpGet("{movieId}")]
        public async Task<ActionResult<Movie>> GetMovie(int movieId)
        {

            var reviews = await _context.Reviews.Where(m => m.MovieID == movieId).ToListAsync();
            var movie = await _context.Movies.FindAsync(movieId);
            movie.Reviews = reviews;

            if (movie == null)
            {
                return NotFound();
            }
            return Ok(movie);
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Movie>>> SearchMovies([FromQuery] string genre)
        {
            var movies = await _context.Movies.Where(m => m.Genre == genre).ToListAsync();
            foreach (var movie in movies)
            {
                var rev = await _context.Reviews.Where(m => m.MovieID == movie.MovieID).ToListAsync();
                movie.Reviews = rev;
            }

            return Ok(movies);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetMovie", new { movieId = movie.MovieID }, movie);
        }
    }
}
