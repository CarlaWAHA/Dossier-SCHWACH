using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using backend.Models;

namespace backend.Controllers
{
    [ApiController]
    [Route("api/movies/{movieId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/comments
        [HttpPost]
public async Task<ActionResult<Comment>> CreateComment(int movieId, Comment comment)
{
    var movie = await _context.Movies.FindAsync(movieId);
    if (movie == null)
        return NotFound("Film non trouvé.");

    comment.MovieId = movieId;
    comment.CreatedAt = DateTime.UtcNow;

    _context.Comments.Add(comment);
    await _context.SaveChangesAsync();

    return CreatedAtAction(nameof(GetComment), new { id = comment.Id }, comment);
}


        // GET: api/comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comment>>> GetAllComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Comment>> GetComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            return comment;
        }

        // GET: api/comments/movie/5
        [HttpGet("movie/{id}")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsForMovie(int movieId)
        {
            return await _context.Comments
                .Where(c => c.MovieId == movieId)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();
        }

        // DELETE: api/comments/5 (optionnel)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            var comment = await _context.Comments.FindAsync(id);
            if (comment == null)
                return NotFound();

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
