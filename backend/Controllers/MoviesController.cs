using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public MoviesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/movies
       [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies
                .Include(m => m.Actors)
                .Include(m => m.Comments)
                .Include(m => m.Ratings)
                .ToListAsync();
        }


        // GET: api/movies/5
        [HttpGet("{id}")]
            public async Task<ActionResult<Movie>> GetMovie(int id)
            {
                var movie = await _context.Movies
                    .Include(m => m.Actors)
                    .Include(m => m.Comments)
                    .Include(m => m.Ratings)
                    .FirstOrDefaultAsync(m => m.Id == id);

                if (movie == null)
                    return NotFound();

                return movie;
            }


        // POST: api/movies
        [HttpPost]
        public async Task<ActionResult<Movie>> CreateMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
        }

        // PUT: api/movies/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id, Movie movie)
        {
            if (id != movie.Id)
                return BadRequest();

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Movies.AnyAsync(e => e.Id == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        
        // DELETE: api/movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
                return NotFound();

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadMovieWithFiles([FromForm] MovieUploadDto movieDto)
        {
            var posterPath = Path.Combine("wwwroot/posters", movieDto.PosterFile.FileName);
            var trailerPath = Path.Combine("wwwroot/trailers", movieDto.TrailerFile.FileName);

        // Save files to disk
        using (var posterStream = new FileStream(posterPath, FileMode.Create))
            await movieDto.PosterFile.CopyToAsync(posterStream);

        using (var trailerStream = new FileStream(trailerPath, FileMode.Create))
            await movieDto.TrailerFile.CopyToAsync(trailerStream);

        // Create Movie
        var movie = new Movie
            {
                Title = movieDto.Title,
                Summary = movieDto.Summary,
                PosterUrl = $"/posters/{movieDto.PosterFile.FileName}",
                TrailerUrl = $"/trailers/{movieDto.TrailerFile.FileName}"
            };

         _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return Ok(movie);
        }
                // 👍 Ajouter un like
        [HttpPost("{id}/like")]
        public async Task<IActionResult> Like(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            movie.Likes++;
            await _context.SaveChangesAsync();
            return Ok(new { likes = movie.Likes });
        }

        // 👍 Retirer un like
        [HttpPost("{id}/unlike")]
        public async Task<IActionResult> Unlike(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null || movie.Likes <= 0) return NotFound();

            movie.Likes--;
            await _context.SaveChangesAsync();
            return Ok(new { likes = movie.Likes });
        }

        // 👎 Ajouter un dislike
        [HttpPost("{id}/dislike")]
        public async Task<IActionResult> Dislike(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            movie.Dislikes++;
            await _context.SaveChangesAsync();
            return Ok(new { dislikes = movie.Dislikes });
        }

        // 👎 Retirer un dislike
        [HttpPost("{id}/undislike")]
        public async Task<IActionResult> Undislike(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null || movie.Dislikes <= 0) return NotFound();

            movie.Dislikes--;
            await _context.SaveChangesAsync();
            return Ok(new { dislikes = movie.Dislikes });
        }

        // 👁️ Incrémenter les vues
        [HttpPost("{id}/view")]
        public async Task<IActionResult> View(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null) return NotFound();

            movie.Views++;
            await _context.SaveChangesAsync();
            return Ok(new { views = movie.Views });
        }

    }
}   
