using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentSubScoreController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public StudentSubScoreController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/StudentSubScore
    [HttpGet]
    public async Task<ActionResult<IEnumerable<StudentSubScore>>> GetStudentSubScores()
    {
        return await _context.StudentSubScores.ToListAsync();
    }

    // GET: api/StudentSubScore/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<StudentSubScore>> GetStudentSubScore(Guid id)
    {
        var score = await _context.StudentSubScores.FindAsync(id);

        if (score == null)
            return NotFound();

        return score;
    }

    // POST: api/StudentSubScore
    [HttpPost]
    public async Task<ActionResult<StudentSubScore>> PostStudentSubScore(StudentSubScore score)
    {
        _context.StudentSubScores.Add(score);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetStudentSubScore), new { id = score.Id }, score);
    }

    // PUT: api/StudentSubScore/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutStudentSubScore(Guid id, StudentSubScore score)
    {
        if (id != score.Id)
            return BadRequest();

        _context.Entry(score).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!StudentSubScoreExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/StudentSubScore/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudentSubScore(Guid id)
    {
        var score = await _context.StudentSubScores.FindAsync(id);
        if (score == null)
            return NotFound();

        _context.StudentSubScores.Remove(score);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool StudentSubScoreExists(Guid id)
    {
        return _context.StudentSubScores.Any(e => e.Id == id);
    }

    // POST: api/StudentSubScore/Bulk
    [HttpPost("Bulk")]
    public async Task<IActionResult> PostBulkScores(List<StudentSubScore> scores)
    {
        if (scores == null || !scores.Any())
            return BadRequest("No scores provided.");

        _context.StudentSubScores.AddRange(scores);
        await _context.SaveChangesAsync();

        return Ok(scores.Count);
    }

}
