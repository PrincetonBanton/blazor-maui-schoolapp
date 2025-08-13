using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolApp.Server.Data;
using SchoolApp.Shared.Models;

namespace SchoolApp.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubGradingScoreController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public SubGradingScoreController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/SubGradingScore
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SubGradingScore>>> GetSubGradingScores()
    {
        return await _context.SubGradingScores.ToListAsync();
    }

    // GET: api/SubGradingScore/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<SubGradingScore>> GetSubGradingScore(Guid id)
    {
        var score = await _context.SubGradingScores.FindAsync(id);

        if (score == null)
            return NotFound();

        return score;
    }

    // POST: api/SubGradingScore
    [HttpPost]
    public async Task<ActionResult<SubGradingScore>> PostSubGradingScore(SubGradingScore score)
    {
        _context.SubGradingScores.Add(score);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetSubGradingScore), new { id = score.Id }, score);
    }

    // PUT: api/SubGradingScore/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> PutSubGradingScore(Guid id, SubGradingScore score)
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
            if (!SubGradingScoreExists(id))
                return NotFound();
            else
                throw;
        }

        return NoContent();
    }

    // DELETE: api/SubGradingScore/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubGradingScore(Guid id)
    {
        var score = await _context.SubGradingScores.FindAsync(id);
        if (score == null)
            return NotFound();

        _context.SubGradingScores.Remove(score);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool SubGradingScoreExists(Guid id)
    {
        return _context.SubGradingScores.Any(e => e.Id == id);
    }

    // POST: api/SubGradingScore/Bulk
    [HttpPost("Bulk")]
    public async Task<IActionResult> PostBulkScores(List<SubGradingScore> scores)
    {
        if (scores == null || !scores.Any())
            return BadRequest("No scores provided.");

        _context.SubGradingScores.AddRange(scores);
        await _context.SaveChangesAsync();

        return Ok(scores.Count);
    }
}
